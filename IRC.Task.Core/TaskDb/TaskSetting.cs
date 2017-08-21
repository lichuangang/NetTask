using QCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.TaskDb
{
    /* ==============================================================================
     * 描述：TaskSetting
     * 创建人：李传刚 2017/7/25 10:54:24
     * ==============================================================================
     */
    public class TaskSetting : IdKey<int>
    {
        public int TaskInfoId { get; set; }

        /// <summary>
        /// 任务类型：0-手工任务 ,1-Cron定时任务
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 调度项名称
        /// </summary>
        public string ScheduleName { get; set; }

        /// <summary>
        /// 部署到的IP地址
        /// </summary>
        public string ServerIP { get; set; }

        public string CronExpression { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 最后一次运行时间
        /// </summary>
        public DateTime RunLastTime { get; set; }

        /// <summary>
        /// 运行次数
        /// </summary>
        public int RunningTimes { get; set; }

        /// <summary>
        /// 多线程执行时配置参数
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// 业务所需参数
        /// </summary>
        public string BusinessParameter { get; set; }

        /// <summary>
        /// 任务当前状态:0-停止,1-运行中,2-就绪状态(定时任务启动后会进入就绪状态)，99-正在停止中
        /// </summary>
        public int Status { get; set; }

        public string Desc { get; set; }

        public DateTime CreateTime { get; set; }


        public string TaskName { get; set; }

        public string BusinessName { get; set; }
    }
}
