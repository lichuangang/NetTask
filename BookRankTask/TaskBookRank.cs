

using System;
using BookRankTask.Models.Enums;
using BookRankTask.Strategies;
using QCommon.Components;
using QCommon.Logging;
using Task.Common;

namespace BookRankTask
{
    class TaskBookRank : BaseTask
    {
        public TaskBookRank()
        {
            StartController.Init();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(TaskBookRank).FullName);
        }
        private readonly ILogger _logger;

        public override void Process()
        {
            _logger.Info("开始BookRankJob");
            HandleRankGenerate();
            _logger.Info("结束BookRankJob");
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
