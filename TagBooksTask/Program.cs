using System;
using QCommon.Cache;
using QCommon.Components;
using QCommon.Configurations;
using QCommon.Logging;

namespace TagBooksTask
{
    internal class Program
    {
        private static ILogger _logger;
        private static void Main(string[] args)
        {
            Configuration
             .Create()
             .UseAutofac()
             .UseLog4Net()
             .UseJsonNet()
             .UseRedisCache()
             .RegisterUnhandledExceptionHandler();

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Program).FullName);
            _logger.Info("初始化完成...");

            TaskScheduler.Start();

            _logger.Info("开始执行任务...");

            Console.ReadLine();
        }
    }
}
