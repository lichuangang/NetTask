using Newtonsoft.Json;
using QCommon.Components;
using QCommon.Logging;
using TagBookTask.DAOS;
using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public abstract class BaseStrategy : IStrategy
    {
        private readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(BaseStrategy).FullName);
        public abstract void DoHandleBegin(TagBook tagBook);
        public abstract void DoHandleEnd(HomeBookTime tagBook);

        public void HandleBegin(TagBook tagBook)
        {
            _logger.InfoFormat("开始优惠处理，参数：【{0}】", JsonConvert.SerializeObject(tagBook));
            DoHandleBegin(tagBook);
            TagBookDao.CompleteStatus(tagBook.Id);
            _logger.InfoFormat("优惠处理完成，书籍id：【{0}】", tagBook.BookId);
        }

        public void HandleEnd(HomeBookTime tagBook)
        {
            _logger.InfoFormat("开始优惠结束，参数：【{0}】", JsonConvert.SerializeObject(tagBook));
            DoHandleEnd(tagBook);
            TagBookDao.DeleteHomeBooks(tagBook.BookId);
            _logger.InfoFormat("优惠结束完成，书籍id：【{0}】", tagBook.BookId);
        }

        public void HandleDeleted(TagBook tagBook)
        {
            _logger.InfoFormat("开始清理，参数：【{0}】", JsonConvert.SerializeObject(tagBook));
            DoHandleEnd(new HomeBookTime
            {
                BookId = tagBook.BookId,
                BookScore = tagBook.Score,
                EndTime = tagBook.EndTime,
                IntFlag2 = tagBook.Flag
            });
            TagBookDao.DeleteHomeBooks(tagBook.BookId);
            TagBookDao.CompleteStatus(tagBook.Id);
            _logger.InfoFormat("清理完成，书籍id：【{0}】", tagBook.BookId);
        }
    }

    public abstract class BaseHomeBookStrategy : BaseStrategy
    {
        public abstract void DoUpdateHandle(TagBook tagBook);

        public override void DoHandleBegin(TagBook tagBook)
        {
            UpdateHomeBook(tagBook);
            DoUpdateHandle(tagBook);
        }

        protected void UpdateHomeBook(TagBook tagBook)
        {
            if (TagBookDao.IsExsitHomeTime(tagBook.BookId))
            {
                TagBookDao.UpdateHomeTime(tagBook.BookId, tagBook.EndTime, tagBook.Score, tagBook.Flag);
            }
            else
            {
                TagBookDao.InsertHomeTime(tagBook.BookId, tagBook.EndTime, tagBook.Score, tagBook.Flag);
            }
        }
    }
}
