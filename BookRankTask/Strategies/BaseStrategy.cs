using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;
using BookRankTask.Models.Mappings;
using QCommon.Components;
using QCommon.Extentions;
using QCommon.Logging;

namespace BookRankTask.Strategies
{
    public abstract class BaseStrategy
    {
        private readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(BaseStrategy).FullName);
        protected void WriteInBookRank(string rankId, List<string> bookIds, RankType rankType)
        {
            _logger.InfoFormat("开始生成排行榜单数据，参数：rankId =>【{0}】,bookIds =>【{1}】,rankType =>【{2}】", rankId, bookIds.Serialize(), rankType);
            bookIds.ForEach(bookId =>
            {
                BookRank bookRank = BookRankDao.GetSingle(bookId, rankId, (int)rankType);
                if (bookRank == null)
                {
                    BookRankDao.Insert(bookId, rankId, rankType);
                }
                else
                {
                    BookRankDao.Update(bookRank);
                }
            });
            _logger.Info("生成排行榜单数据完成");
        }
    }
}
