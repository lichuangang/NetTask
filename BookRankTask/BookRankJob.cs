using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;
using BookRankTask.Strategies;
using QCommon.Components;
using QCommon.Logging;
using Quartz;

namespace BookRankTask
{
    public class BookRankJob : IJob
    {
        private readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(BookRankJob).FullName);

        public void Execute(IJobExecutionContext context)
        {
            _logger.Info("开始BookRankJob");
            Process();
            _logger.Info("结束BookRankJob");
        }

        public void Process()
        {
            HandleRankGenerate();
        }
        private void HandleRankGenerate()
        {
            foreach (BigTagType bigTagType in Enum.GetValues(typeof(BigTagType)))
            {
                var strategy = StrategyFactory.GetStrategy(bigTagType);
                strategy.HandleRankGenerate();
            }
        }
    }
}
