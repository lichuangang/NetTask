using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task.Common;
using Task.Common.Utilities;
using TaskHost.Dtos;

namespace TaskHost
{
    /* ==============================================================================
     * 描述：TaskContainer
     * 创建人：李传刚 2017/8/1 13:04:32
     * ==============================================================================
     */
    public class TaskContainer
    {
        #region 字段

        private const BindingFlags bindFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance;

        /// <summary>
        /// 任务参数
        /// </summary>
        private TaskParameter _taskParam;

        /// <summary>
        /// 任务dll文件全路径
        /// </summary>
        private string _AssemblyFile;

        /// <summary>
        /// 应用程序域创建对象的工厂类
        /// </summary>
        private AppDomainObjectProxyFactory<BaseTask> proxyFactory = null;

        #endregion

        #region 构造
        public TaskContainer(TaskParameter taskParam)
        {
            _taskParam = taskParam;
            _AssemblyFile = Path.Combine(taskParam.TaskRootPath, taskParam.TaskAssemblyName);
            if (!File.Exists(_AssemblyFile))
            {
                throw new Exception("文件不存在!");
            }
            proxyFactory = new AppDomainObjectProxyFactory<BaseTask>(_AssemblyFile);
        }
        #endregion

        /// <summary>
        /// 执行任务
        /// </summary>
        public void StartTask()
        {
            proxyFactory.Proxy.Process();
        }
    }
}
