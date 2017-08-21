using QCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.TaskDb
{
    /* ==============================================================================
     * 描述：TaskRunLog
     * 创建人：李传刚 2017/7/25 11:32:36
     * ==============================================================================
     */
    public class TaskRunLog:IdKey<long>
    {
        public int SettingId { get; set; }

        public string TaskName { get; set; }

        public string ServerIP { get; set; }


        public DateTime StartTime { get; set; }

        public sbyte ThreadCount { get; set; }

        public string BusinessParameter { get; set; }

        /// <summary>
        /// 执行状态:0-正在执行中,1-已执行完,2-执行异常未运行,3-执行被异常中断
        /// </summary>
        public sbyte ExecuteStatus { get; set; }

        /// <summary>
        /// 执行结束时间
        /// 当为1970-01-01时代表还在执行中
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行是否成功:0-失败,1-成功
        /// </summary>
        public sbyte IsSuccess { get; set; }

        public string ResultMessage { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
