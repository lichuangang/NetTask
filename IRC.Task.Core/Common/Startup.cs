using QCommon.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Common
{
    /* ==============================================================================
     * 描述：Startup
     * 创建人：李传刚 2017/7/25 14:05:31
     * ==============================================================================
     */
    public class Startup
    {
        /// <summary>
        /// 初始化相关加载配置
        /// </summary>
        public static void InitConfiguration(params Assembly[] assemyblys)
        {
            var assemblies = new[]
            {
                Assembly.Load("QCommon"),
                Assembly.Load("IRC.Task.Repositories"),
                Assembly.Load("IRC.Task.Applications"),
                Assembly.Load("IRC.Task.QueryServices"),
                Assembly.Load("IRC.Task.Core")                
            };

            assemblies = assemblies.Concat(assemyblys).ToArray();

            Configuration.Create()
                .UseAutofac()
                .UseJsonNet()
                .UseLog4Net()
                .RegisterBusinessComponents(assemblies)
                .RegisterInterceptors(assemblies)
                .EnableInterceptors(assemblies)
                .InitializeBusinessAssemblies(assemblies);
        }
    }
}
