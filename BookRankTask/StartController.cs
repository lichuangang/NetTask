using QCommon.Cache;
using QCommon.Components;
using QCommon.Configurations;
using QCommon.Logging;
namespace BookRankTask
{
    public class StartController
    {
        private static bool _started;

        private static ILogger _logger;
        public static void Init()
        {
            if (!_started)
            {
                Configuration
                    .Create()
                    .UseAutofac()
                    .UseLog4Net()
                    .UseJsonNet()
                    .UseRedisCache()
                    .RegisterUnhandledExceptionHandler();
                _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(StartController).FullName);
                _logger.Info("初始化完成...");
                _started = true;
            }
        }
    }
}
