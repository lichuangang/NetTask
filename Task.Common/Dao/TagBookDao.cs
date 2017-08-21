using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using Task.Common.Mapping;

namespace Task.Common.Dao
{
    /* ==============================================================================
     * 描述：TagBookDao
     * 创建人：李传刚 2017/7/31 14:29:33
     * ==============================================================================
     */
    public class TagBookDao
    {
        private static string logSql = @"INSERT INTO EditorActionDetail (UserID,BookID,CreateDate,UserActionDetail) VALUES(
                                                                    @UserID,@BookID,@CreateDate,@UserActionDetail)";
        public static List<TagBook> GetNeedRecordsTagBooks()
        {
            //3 4 101 现在只有这三种类型需要记录到homebooktimes
            var sql = @"SELECT * from TagBooks WHERE Flag in (3,4,101,200) AND Status=0 AND BeginTime <= @dateTime ";
            using (var connection = BaseDao.GetGoodBooksConnection())
            {
                return connection.Query<TagBook>(sql, new { dateTime = DateTime.Now }).ToList();
            }
        }

        public static void UpdateBook(Book book, TagBook tagBook)
        {
            using (var connection = BaseDao.GetGoodBooksConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                //1.修改TagBooks.Status=1 ,UpdateTime
                connection.Execute("UPDATE TagBooks SET Status=1,UpdateTime=getdate() WHERE Id=@Id", new { Id = tagBook.Id });
                //2.修改Book.BookScore ,IntFlag2
                if (string.IsNullOrEmpty(tagBook.BookScore) || tagBook.Flag != 4)
                {
                    //不改价格
                    connection.Execute("UPDATE Book SET IntFlag2=@IntFlag2,BookLastUpdateDate=getdate() WHERE BookId=@BookId", new { IntFlag2 = tagBook.Flag, BookId = tagBook.BookId });
                }
                else
                {
                    connection.Execute("UPDATE Book SET BookScore=@Score,IntFlag2=@IntFlag2,BookLastUpdateDate=getdate() WHERE BookId=@BookId", new { Score = int.Parse(tagBook.BookScore), IntFlag2 = tagBook.Flag, BookId = tagBook.BookId });
                }
                //3.记录HomeBookTimes
                var insertSql = @"  INSERT INTO HomeBookTimes 
                                    (BookID,EndTime,BookScore,LastUpDate,IntFlag2) VALUES
                                    ( @BookID,@EndTime,@BookScore,@LastUpDate,@IntFlag2)";
                connection.Execute(insertSql, new
                {
                    BookID = tagBook.BookId,
                    EndTime = tagBook.EndTime,
                    BookScore = book.BookScore,
                    LastUpDate = DateTime.Now,
                    IntFlag2 = tagBook.Flag
                });

                //4.记录操作日志        
                connection.Execute(logSql, new
                {
                    BookID = tagBook.BookId,
                    UserID = "TaskAdmin",
                    CreateDate = DateTime.Now,
                    UserActionDetail = string.IsNullOrEmpty(tagBook.BookScore) ? string.Format("修改书籍的状态为{0}", tagBook.Flag) : string.Format("修改书籍的价格为{0}，全本书籍的状态为{1}", tagBook.BookScore, tagBook.Flag)
                });
            }
        }

        public static List<HomeBookTime> GetExpireHomeBookTime()
        {
            var sql = "SELECT * FROM HomeBookTimes WHERE EndTime <=getdate()";
            using (var connection = BaseDao.GetGoodBooksConnection())
            {
                return connection.Query<HomeBookTime>(sql).ToList();
            }
        }

        public static void RestoreBook(Book book, HomeBookTime homeBookTime)
        {
            using (var connection = BaseDao.GetGoodBooksConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                //1.删除HomeBookTime记录
                connection.Execute("DELETE FROM  HomeBookTimes WHERE BookID=@BookID", new { BookID = homeBookTime.BookID });
                //2.修改Book.BookScore ,IntFlag2
                if (homeBookTime.IntFlag2 == 4)
                {
                    //价格不等。执行修改
                    connection.Execute("UPDATE Book SET BookScore=@Score,IntFlag2=@IntFlag2,BookLastUpdateDate=getdate() WHERE BookId=@BookId", new { Score = homeBookTime.BookScore, IntFlag2 = 0, BookId = book.BookID });
                }
                else
                {
                    connection.Execute("UPDATE Book SET IntFlag2=@IntFlag2,BookLastUpdateDate=getdate() WHERE BookId=@BookId", new { IntFlag2 = 0, BookId = book.BookID });
                }

                //3.记录操作日志
                connection.Execute(logSql, new
                {
                    BookID = book.BookID,
                    UserID = "TaskAdmin",
                    CreateDate = DateTime.Now,
                    UserActionDetail = homeBookTime.IntFlag2 == 4 ? string.Format("修改书籍的价格为{0}，全本书籍的状态为{1}", homeBookTime.BookScore, 0) : string.Format("修改书籍的状态为{0}", 0)
                });
            }
        }
    }
}
