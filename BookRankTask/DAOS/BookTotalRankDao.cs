using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.Models.Enums;

namespace BookRankTask.DAOS
{
    public class BookTotalRankDao : BaseDapperDao
    {
        /// <summary>
        /// 【刘学龙 2017-08-11 133725】 获取男生、女生、出版下的畅销书籍
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType">分：周、月、总 3个榜单</param>
        /// <returns></returns>
        public static List<string> GetGoodSellBookIds(string categoryId, RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekCoin desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthCoin desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalCoin desc ";
            var sql = @"select top 25 b.bookID 
                           from Book b 
                           right join BookTotalRank br on b.BookID = br.BookId 
                           left join BookCategory bc on bc.BookID = b.BookID 
                           left join Category c on c.NewCategoryID = bc.CategoryID 
                           where b.BookStatus = 1  and b.intFlag1 > 0  and c.NewPID = @categoryId" + strSqlOrderBy;
            return QueryList<string>(sql, new
            {
                categoryId = categoryId
            }) as List<string>;
        }
        /// <summary>
        /// 获取完结榜书籍Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType"></param>
        /// <returns></returns>
        public static List<string> GetEndBookIds(string categoryId, RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekCoin desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthCoin desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalCoin desc ";
            var sql = @"select top 25 b.bookID 
                        from Book b 
                        right join BookTotalRank br on b.BookID=br.BookId 
                        left join BookCategory bc on bc.BookID=b.BookID 
                        left join Category c on c.NewCategoryID=bc.CategoryID 
                        where b.WriteStatus=102  and b.BookStatus=1 and b.intFlag1>0 and c.NewPID = @categoryId" + strSqlOrderBy;
            return QueryList<string>(sql, new
            {
                categoryId = categoryId
            }) as List<string>;
        }
        /// <summary>
        /// 获取新书榜
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType"></param>
        /// <returns></returns>
        public static List<string> GetNewBookIds(string categoryId, RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalReadUserCount desc ";
            var sql = @"select top 25 b.bookID 
                        from Book b 
                        right join BookTotalRank br on b.BookID=br.BookId 
                        left join BookCategory bc on bc.BookID=b.BookID 
                        left join Category c on c.NewCategoryID=bc.CategoryID 
                        where  BookStatus=1 
                               and intFlag1>0 
                               and BookWords < 500000 
                               and BookScore>0 
                               and WriteStatus = 101 
                               and ChapterLastUpdate >@chapterLastUpdate 
                               and c.NewPID= @categoryId" + strSqlOrderBy;
            return QueryList<string>(sql, new
            {
                categoryId = categoryId,
                chapterLastUpdate = DateTime.Now.AddDays(-7)
            }) as List<string>;
        }
        public static List<string> GetFreeBookIds(string categoryId, RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalReadUserCount desc ";
            var sql = @"select top 25 b.bookID 
                        from Book b 
                        right join BookTotalRank br on b.BookID=br.BookId 
                        left join BookCategory bc on bc.BookID=b.BookID 
                        left join Category c on c.NewCategoryID=bc.CategoryID 
                        where  BookStatus=1 
                               and intFlag1>0 
                               and BookWords < 500000 
                               and (BookScore=0 or BookScore=null) 
                               and WriteStatus = 101 
                               and ChapterLastUpdate >@chapterLastUpdate and c.NewPID= @categoryId" + strSqlOrderBy;
            return QueryList<string>(sql, new
            {
                categoryId = categoryId,
                chapterLastUpdate = DateTime.Now.AddDays(-7)
            }) as List<string>;
        }
        /// <summary>
        /// 获取出版新书
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType"></param>
        /// <returns></returns>
        public static List<string> GetPublishNewBookIds(string categoryId, RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalReadUserCount desc ";
            var sql = @"select top 25 b.bookID 
                        from Book b 
                        right join BookTotalRank br on b.BookID=br.BookId 
                        left join BookCategory bc on bc.BookID=b.BookID 
                        left join Category c on c.NewCategoryID=bc.CategoryID 
                        where  BookStatus=1 
                               and intFlag1>0 
                               and  AuditDate >@auditDate 
                               and c.NewPID= @categoryId" + strSqlOrderBy;
            return QueryList<string>(sql, new
            {
                categoryId = categoryId,
                auditDate = DateTime.Now.AddDays(-90)
            }) as List<string>;
        }
        /// <summary>
        /// 获取出版口碑书籍
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType"></param>
        /// <returns></returns>
        public static List<string> GetPublishGoodBookIds(string categoryId)
        {
            var sql = @"select top 100 BookId,(t.bcCount + t.BookAverageRating) as zh 
                        from(select  b.BookID,(select COUNT(1) from BookComments where BookID = b.BookID)as bcCount ,b.BookAverageRating 
		                     from Book b right join BookTotalRank br on b.BookID=br.BookId 
		                                            left join BookCategory bc on bc.BookID=b.BookID 
		                                            left join Category c on c.NewCategoryID=bc.CategoryID 
		                                            where  BookStatus=1 
		                                            and intFlag1>0   
		                                            and c.NewPID=@categoryId
		                                            --order by bcCount desc,b.BookAverageRating desc
                                            ) as t order by zh desc";
            return QueryList<string>(sql, new
            {
                categoryId = categoryId
            }) as List<string>;
        }
        /// <summary>
        /// 获取Vip人气榜
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="rankType"></param>
        /// <returns></returns>
        public static List<string> GetVipPopularBookIds( RankType rankType)
        {
            var strSqlOrderBy = string.Empty;
            if (rankType == RankType.Week)
                strSqlOrderBy = " order by WeekReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by MonthReadUserCount desc ";
            else if (rankType == RankType.Week)
                strSqlOrderBy = " order by TotalReadUserCount desc ";
            var sql = @"select top 25 b.bookID 
                        from Book b 
                        right join BookTotalRank br on b.BookID=br.BookId 
                        where b.intFlag2 = 200 
                              and b.BookStatus = 1 
                              and b.intFlag1 > 0" + strSqlOrderBy;
            return QueryList<string>(sql) as List<string>;
        }
    }
}
