using IRC.Task.Core.Dtos;
using QCommon.Components;
using QCommon.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCommon.Extentions;
using Quartz;
using System.IO;

namespace IRC.Task.Core.Common
{
    /* ==============================================================================
     * 描述：TaskProcessPool
     * 创建人：李传刚 2017/7/26 11:28:46
     * ==============================================================================
     */
    public class TaskProcessPool
    {
        #region 字段
        private ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(TaskProcessPool).FullName);
        /// <summary>
        /// 任务宿主的进程名称
        /// </summary>
        private const string TaskHostName = "IRC.Task.Host";

        /// <summary>
        /// 任务宿主的文件名 xxx.exe
        /// </summary>
        public readonly string TaskHostFile = ConfigSettings.TaskHostFile;

        private static Dictionary<int, Process> processDict = new Dictionary<int, Process>();

        private static TaskProcessPool _instance = null;
        private TaskProcessPool() { }
        public static TaskProcessPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskProcessPool();
                }

                return _instance;
            }
        }

        #endregion

        public void StartTask(TaskParameter param)
        {
            //直接异步执行
            System.Threading.Tasks.Task.Factory.StartNew<int>(() =>
            {
                return ThreadStartTask(param);
            });
        }

        public void StopTask(int settingId)
        {
            if (processDict.ContainsKey(settingId))
            {
                var process = processDict[settingId];
                //先删除
                processDict.Remove(settingId);
                try
                {
                    //再杀死
                    process.Kill();
                }
                catch (Exception)
                {

                }
            }
        }

        private int ThreadStartTask(TaskParameter param)
        {
            try
            {
                //初始化进程
                ProcessStartInfo ps = new ProcessStartInfo(this.TaskHostFile);
                ps.WorkingDirectory = new FileInfo(this.TaskHostFile).Directory.FullName;
                ps.Arguments = MainParamUtils.Serialize(param);
                ps.UseShellExecute = true;
                //开启新进程,并进行存储
                var process = Process.Start(ps);
                if (process != null)
                {
                    if (processDict.ContainsKey(param.SettingId))
                    {
                        //如果是再次运行，先将原来的杀掉
                        StopTask(param.SettingId);
                    }
                    processDict.Add(param.SettingId, process);
                    return process.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("开启Host.exe进程时出错。", ex);
            }
            return 0;
        }
    }
}
