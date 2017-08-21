using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;

namespace BookRankTask.Strategies
{
    public class PublishStrategy : BaseStrategy, IStrategy
    {
        /// <summary>
        /// 出版畅销榜
        /// </summary>
        private static readonly string _sellingRankId = "314b0fb2f8a947248cedaac585fbba44";
        /// <summary>
        /// 出版新书榜
        /// </summary>
        private static readonly string _newRankId = "314b0fb2f8a947248cedaac585fbba44";
        /// <summary>
        /// 出版口碑榜
        /// </summary>
        private static readonly string _goodRankId = "af717dcce1df49d390bdad2dc3d4efdd";
        /// <summary>
        /// 畅销榜
        /// </summary>
        void GenerateGoodSellBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetGoodSellBookIds(_sellingRankId, rankType);
                WriteInBookRank(_sellingRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 新书榜
        /// </summary>
        void GenerateNewBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetPublishNewBookIds(_newRankId, rankType);
                WriteInBookRank(_newRankId, bookIds, rankType);
            }
        }
        /// <summary>
        /// 口碑榜
        /// </summary>
        void GenerateGoodBooks()
        {
            List<string> bookIds = BookTotalRankDao.GetPublishGoodBookIds(_goodRankId);
            WriteInBookRank(_goodRankId, bookIds, RankType.Week);
        }
        public void HandleRankGenerate()
        {
            GenerateGoodSellBooks();
            GenerateNewBooks();
            GenerateGoodBooks();
        }
    }
}
