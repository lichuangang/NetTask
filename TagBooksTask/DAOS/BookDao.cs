using System.Data;
using System.Text;
using Dapper;

namespace TagBooksTask.DAOS
{
    public class BookDao : BaseDapperDao
    {
        public static int? GetBookScore(string bookId)
        {
            var sql = @"SELECT  BookScore
            FROM    dbo.Book
            WHERE   BookID = @BookId";
            return ExecuteScalar<int?>(sql, new { BookId = bookId });
        }

        public static void UpdateBookIntflag(string bookId, int intflag2, int? bookScore, int? intflag3)
        {
            var p = new DynamicParameters();
            p.Add("BookId", bookId, DbType.AnsiStringFixedLength, size: 50);
            p.Add("Intflag2", intflag2, DbType.Int32);

            var sb = new StringBuilder();
            if (bookScore != null)
            {
                sb.Append(" ,BookScore = @BookScore");
                p.Add("BookScore", bookScore, DbType.Int32);
            }
            if (intflag3 != null)
            {
                sb.Append(" ,intflag3 = @Intflag3");
                p.Add("Intflag3", intflag3, DbType.Int32);
            }

            var sql = string.Format(@"UPDATE  dbo.Book
            SET     intFlag2 = @Intflag2 {0} ,
                    ChapterLastUpdate = GETDATE()
            WHERE   BookID = @BookId", sb);
            Execute(sql, p);
        }
    }
}
