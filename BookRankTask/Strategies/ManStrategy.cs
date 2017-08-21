using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;
using BookRankTask.Models.Mappings;

namespace BookRankTask.Strategies
{
    public class ManStrategy : BaseStrategy, IStrategy
    {
        /// <summary>
        /// 男生畅销榜
        /// </summary>
        private static readonly string _manSellingRankId = "f8446bb12752450386e648da6316071d";
        /// <summary>
        /// 男生完结榜
        /// </summary>
        private static readonly string _manEndRankId = "62f5f49647364569b7ca496d4bc6e6da";
        /// <summary>
        /// 男生新书榜
        /// </summary>
        private static readonly string _manNewRankId = "ded9f0ce1a714248bcfd4aa15c6b746b";
        /// <summary>
        /// 男生免费榜
        /// </summary>
        private static readonly string _manFreeRankId = "newmanfree";
        /// <summary>
        /// 畅销榜
        /// </summary>
        void GenerateGoodSellBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetGoodSellBookIds(_manSellingRankId, rankType);
                WriteInBookRank(_manSellingRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 完结榜
        /// </summary>
        void GenerateEndBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetEndBookIds(_manEndRankId, rankType);
                WriteInBookRank(_manEndRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 新书榜 
        /// </summary>
        void GenerateNewBooks()
        {
            List<string> bookIds = BookTotalRankDao.GetNewBookIds(_manNewRankId, RankType.Week);
            WriteInBookRank(_manNewRankId, bookIds, RankType.Week);
        }
        /// <summary>
        /// 免费榜
        /// </summary>
        void GenerateFreeBooks()
        {
            List<string> bookIds = BookTotalRankDao.GetFreeBookIds(_manFreeRankId, RankType.Week);
            WriteInBookRank(_manFreeRankId, bookIds, RankType.Week);
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
