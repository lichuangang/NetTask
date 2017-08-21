using System;
using QCommon.Components;
using QCommon.Logging;
using QCommon.Utilities;
using TagBookTask.DAOS;
using TagBookTask.Models;
using TagBookTask.Strategies;
using Task.Common;

namespace TagBookTask
{
    public class TagBookTask : BaseTask
    {
        public TagBookTask()
        {
            StartController.Init();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(TagBookTask).FullName);
        }

        private readonly ILogger _logger;

        public override void Process()
        {
            try
            {
                _logger.Info("开始TagBookJob");
                HandleBegin();
                HandleEnd();
                _logger.Info("结束TagBookJob");

                HandleDeleted();
                _logger.Info("清理TagBookJob完成");
            }
            catch (Exception exception)
            {
                _logger.Error("tag任务异常", exception);
                Utils.SendException("tag任务异常", exception.ToString());
            }

        }

        private void HandleDeleted()
        {
            var tagBooks = TagBookDao.GetDeletedTagBooks();
            foreach (var tagBook in tagBooks)
            {
                var preferentialType = Convert2Enum<PreferentialType>(tagBook.Flag);
                var strategy = StrategyFactory.GetStrategy(preferentialType);
                strategy.HandleDeleted(tagBook);
            }
        }

        private void HandleBegin()
        {
            var tagBooks = TagBookDao.GetBeginTagBooks();
            foreach (var tagBook in tagBooks)
            {
                var preferentialType = Convert2Enum<PreferentialType>(tagBook.Flag);
                var strategy = StrategyFactory.GetStrategy(preferentialType);
                strategy.HandleBegin(tagBook);
            }

        }

        private void HandleEnd()
        {
            var homeBookTimes = TagBookDao.GetEndTagBooks();
            foreach (var bookTime in homeBookTimes)
            {
                var preferentialType = Convert2Enum<PreferentialType>(bookTime.IntFlag2);
                var strategy = StrategyFactory.GetStrategy(preferentialType);
                strategy.HandleEnd(bookTime);
            }

        }

        public static T Convert2Enum<T>(object obj) where T : struct, IComparable, IFormattable, IConvertible
        {
            T t;
            Enum.TryParse(obj.ToString(), true, out t);
            return t;
        }
    }
}
