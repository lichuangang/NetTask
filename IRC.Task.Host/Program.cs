using IRC.Task.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCommon.Extentions;
using System.Reflection;
using QCommon.Configurations;
using QCommon.Components;
using IRC.Task.Core.Common;
using QCommon.Logging;
using QCommon.ThirdParty.Autofac;
using Autofac;
using IRC.Task.Repositories;
using System.Diagnostics;
using IRC.Task.Applications;

namespace IRC.Task.Host
{
    /// <summary>
    /// 程序执行宿主
    /// </summary>
    class Program
    {
        static ILogger _logger;
        private static void Init()
        {
            Startup.InitConfiguration(Assembly.Load("IRC.Task.Host"));
            RegisterBaseRepositorys();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("IRC.Task.Host");
        }

        /// <summary>
        /// 注册TaskDbRepository 
        /// </summary>
        private static void RegisterBaseRepositorys()
        {
            var container = ((AutofacObjectContainer)ObjectContainer.Current).Container;
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(TaskDbRepository<>));
            builder.Update(container);
        }

        static void Main(string[] args)
        {
            try
            {
                Init();
                Console.WriteLine("开始执行任务:" + DateTime.Now.ToShortTimeString());
                var param = MainParamUtils.Deserialize(args);
                _logger.InfoFormat("开始执行任务:{0}", param.TaskAssemblyName);
                Invoke(param);
                Console.WriteLine("执行任务结束:" + DateTime.Now.ToShortTimeString());
                _logger.InfoFormat("执行任务结束:{0}", param.TaskAssemblyName);
                if (param.TaskType == 1)
                {
                    //定时任务状态改为就绪
                    ObjectContainer.Resolve<ITaskRunService>().RestQuartzTask(param.SettingId);
                }
                else
                {
                    ObjectContainer.Resolve<ITaskRunService>().StopTask(param.SettingId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("执行任务出现异常:" + string.Join(",", args), ex);
            }

        }

        private static void Invoke(TaskParameter param)
        {
            new TaskContainer(param).StartTask();
        }
    }
}
