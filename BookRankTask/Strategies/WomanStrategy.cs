using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;

namespace BookRankTask.Strategies
{
    public class WomanStrategy : BaseStrategy, IStrategy
    {
        /// <summary>
        /// 女生畅销榜
        /// </summary>
        private static readonly string _woManSellingRankId = "8d0a75ac29a64105b00b5c1bfa863eec";
        /// <summary>
        /// 女生完结榜
        /// </summary>
        private static readonly string _woManEndRankId = "f134abb05b854be394dd0091f918af41";
        /// <summary>
        /// 女生新书榜
        /// </summary>
        private static readonly string _woManNewRankId = "790f0a5220b748c5b8cf5fa7af8db352";
        /// <summary>
        /// 女生免费榜
        /// </summary>
        private static readonly string _woManFreeRankId = "newwomanfree";
        /// <summary>
        /// 畅销榜
        /// </summary>
        void GenerateGoodSellBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetGoodSellBookIds(_woManSellingRankId, rankType);
                WriteInBookRank(_woManSellingRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 完结榜
        /// </summary>
        void GenerateEndBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetEndBookIds(_woManEndRankId, rankType);
                WriteInBookRank(_woManEndRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 新书榜
        /// </summary>
        void GenerateNewBooks()
        {
            List<string> bookIds = BookTotalRankDao.GetNewBookIds(_woManNewRankId, RankType.Week);
            WriteInBookRank(_woManNewRankId, bookIds, RankType.Week);
        }
        /// <summary>
        /// 免费榜
        /// </summary>
        void GenerateFreeBooks()
        {
            List<string> bookIds = BookTotalRankDao.GetFreeBookIds(_woManFreeRankId, RankType.Week);
            WriteInBookRank(_woManFreeRankId, bookIds, RankType.Week);
        }
        /// <summary>
        /// 生成榜单
        /// </summary>
        public void HandleRankGenerate()
        {
            GenerateGoodSellBooks();
            GenerateEndBooks();
            GenerateNewBooks();
            GenerateFreeBooks();
        }
    }
}
