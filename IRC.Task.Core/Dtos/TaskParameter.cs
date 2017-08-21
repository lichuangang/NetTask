using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Dtos
{
    /* ==============================================================================
     * 描述：TaskParameter
     * 创建人：李传刚 2017/7/26 10:53:31
     * ==============================================================================
     */
    public class TaskParameter
    {
        public int SettingId { get; set; }
        /// <summary>
        /// 0 单次任务  1 定时任务
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 任务所在的根目录
        /// </summary>
        public string TaskRootPath { get; set; }

        /// <summary>
        /// 待执行任务的程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        public string CronExpression { get; set; }

        /// <summary>
        /// 任务初始化时的业务参数
        /// 为Json字符串,由任务自行负责解析
        /// </summary>
        public string TaskInitParameter { get; set; }

        /// <summary>
        /// 任务当前状态:0-停止,1-运行中,2-就绪状态(定时任务启动后会进入就绪状态)，99-正在停止中
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开启的线程数
        /// </summary>
        public int ThreadCount { get; set; }
    }
}
