using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Applications
{
    public interface ITaskRunService
    {
        /// <summary>
        /// 操作器调用
        /// </summary>
        /// <param name="settingId"></param>
        void StartTask(int settingId);

        void StopTask(int settingId);

        /// <summary>
        /// 如果IIS挂了,重新启动时被调用
        /// </summary>
        void Restart();


        /// <summary>
        /// 定时任务执行
        /// </summary>
        void QuartzRun(int settingId);

        void RestQuartzTask(int settingId);
    }
}
