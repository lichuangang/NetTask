using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.Models.Enums;
using BookRankTask.Models.Mappings;
using QCommon.Utilities;

namespace BookRankTask.DAOS
{
    public class BookRankDao : BaseDapperDao
    {
        public static BookRank Single(string bookId, string rankId, int rankType)
        {
            string sql = @"select * 
                           from BookRank 
                           where bookID=@bookId 
                                 and rankID=@rankId 
                                 and RankType=@rankType";
            return QueryFirst<BookRank>(sql, new
            {
                bookId = bookId,
                rankId = rankId,
                rankType = rankType,
            });
        }
        public static BookRank GetSingle(string bookId, string rankId, int rankType)
        {
            string sql = @"select * 
                           from BookRank 
                           where bookID=@bookId 
                                 and rankID=@rankId 
                                 and RankType=@rankType";
            return QueryList<BookRank>(sql, new
            {
                bookId = bookId,
                rankId = rankId,
                rankType = rankType,
            }).FirstOrDefault();
        }
        public static int Update(BookRank bookRank)
        {
            string sql = @"update bookRank 
                           set createDate = @createDate
                           where bookRankId = @bookRankId";
            return Execute(sql, new
            {
                createDate = DateTime.Now,
                bookRankId = bookRank.BookRankID
            });
        }
        public static int Insert(string bookId, string rankId, RankType rankType)
        {
            string sql = @"insert into BookRank(bookRankId
                                                ,bookID
                                                ,rankID
                                                ,createDate
                                                ,rankType) 
                                        values (@bookrankId
                                                ,@bookId
                                                ,@rankId
                                                ,@createDate
                                                ,@rankType)";
            return Execute(sql, new
            {
                bookRankId = GuidUtility.GetGuid(),
                bookId = bookId,
                rankId = rankId,
                createDate = DateTime.Now,
                rankType = (int)rankType,
            });
        }
    }
}
