using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;

namespace BookRankTask.Strategies
{
    public class OtherStrategy : BaseStrategy, IStrategy
    {
        /// <summary>
        /// VIP人气榜
        /// </summary>
        private static readonly string _popularRankId = "d3446bb12752450386e648da6311035d";
        /// <summary>
        /// 人气榜
        /// </summary>
        void GeneratePopularBooks()
        {
            foreach (RankType rankType in Enum.GetValues(typeof(RankType)))
            {
                List<string> bookIds = BookTotalRankDao.GetVipPopularBookIds(rankType);
                WriteInBookRank(_popularRankId, bookIds, rankType);
            }
        }
        public void HandleRankGenerate()
        {
            GeneratePopularBooks();
        }
    }
}
