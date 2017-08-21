using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.DAOS;
using BookRankTask.Models.Enums;
using QCommon.Cache;
using QCommon.Components;
using QCommon.Configurations;
using QCommon.Logging;

namespace BookRankTask
{
    internal class Program
    {
        private static ILogger _logger;
        static void Main(string[] args)
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
