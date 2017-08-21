using System;
using QCommon.Components;
using QCommon.Logging;
using Quartz;
using TagBooksTask.DAOS;
using TagBooksTask.Models;
using TagBooksTask.Strategies;

namespace TagBooksTask
{
    public class TagBookJob : IJob
    {
        private readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(TagBookJob).FullName);
        public void Execute(IJobExecutionContext context)
        {
            _logger.Info("开始TagBookJob");
            Process();
            _logger.Info("结束TagBookJob");
        }

        public void Process()
        {
            HandleBegin();
            HandleEnd();
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
