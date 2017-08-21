using System;
using System.Collections.Generic;
using System.Linq;
using TagBooksTask.Models;

namespace TagBooksTask.DAOS
{
    public class TagBookDao : BaseDapperDao
    {

        public static List<TagBook> GetBeginTagBooks()
        {
            var sql = @"SELECT * from TagBooks WHERE Status=0 AND BeginTime <= @Now and EndTime>@Now ";
            return QueryList<TagBook>(sql, new
            {
                DateTime.Now
            }).ToList();
        }

        public static List<HomeBookTime> GetEndTagBooks()
        {
            var sql = @"SELECT * from HomeBookTimes WHERE EndTime <= @dateTime ";
            return QueryList<HomeBookTime>(sql, new
            {
                dateTime = DateTime.Now
            }).ToList();
        }

        public static void CompleteStatus(string id)
        {
            var sql = @"UPDATE  dbo.TagBooks
            SET     Status = 1,UpdateTime = GETDATE()
            WHERE   Id = @Id";
            Execute(sql, new { Id = id });
        }

        public static void DeleteHomeBooks(string bookId)
        {
            var sql = @"DELETE  FROM dbo.HomeBookTimes
            WHERE   BookID = @BookId";
            Execute(sql, new { BookId = bookId });
        }

        public static HomeBookTime GetBookTime(string bookId)
        {
            var sql = @"SELECT  BookID ,
                    EndTime ,
                    BookScore ,
                    LastUpDate ,
                    IntFlag2
            FROM    dbo.HomeBookTimes
            WHERE   BookID = @BookId";
            return QueryList<HomeBookTime>(sql, new { BookId = bookId }).First();
        }

        public static void InsertHomeTime(string bookId, DateTime endTime, int? bookScore, int intflag2)
        {
            var sql = @"INSERT INTO dbo.HomeBookTimes
                    ( BookID ,
                      EndTime ,
                      BookScore ,
                      LastUpDate ,
                      IntFlag2
                    )
            VALUES  ( @BookId , -- BookID - varchar(32)
                      @EndTime , -- EndTime - datetime
                      @BookScore , -- BookScore - int
                      @LastUpDate , -- LastUpDate - datetime
                      @IntFlag2  -- IntFlag2 - int
                    )";
            Execute(sql, new
            {
                BookId = bookId,
                EndTime = endTime,
                BookScore = bookScore,
                LastUpDate = DateTime.Now,
                Intflag2 = intflag2
            });
        }

        public static void UpdateHomeTime(string bookId, DateTime endTime, int? bookScore, int intflag2)
        {
            var sql = @"UPDATE  dbo.HomeBookTimes
            SET     EndTime = @EndTime ,
                    BookScore = @BookScore ,
                    LastUpDate = @LastUpdate ,
                    IntFlag2 = @Intflag2
            WHERE   BookID = @BookId";
            Execute(sql, new
            {
                BookId = bookId,
                EndTime = endTime,
                BookScore = bookScore,
                LastUpDate = DateTime.Now,
                Intflag2 = intflag2
            });
        }


        public static bool IsExsitHomeTime(string bookId)
        {
            var sql = @"SELECT  COUNT(1)
            FROM    dbo.HomeBookTimes
            WHERE   BookID = @BookId";
            var count = ExecuteScalar<int>(sql, new { BookId = bookId });
            return count > 0;
        }


    }
}
