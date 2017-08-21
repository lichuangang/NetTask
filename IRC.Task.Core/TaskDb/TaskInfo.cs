using QCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.TaskDb
{
    /* ==============================================================================
     * 描述：TaskInfo
     * 创建人：李传刚 2017/7/25 10:52:00
     * ==============================================================================
     */
    public class TaskInfo : IdKey<int>
    {
        /// <summary>
        /// 任务名称，必需唯一。约定全英文
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }
        
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string TaskAssemblyName { get; set; }

        public string FileName { get; set; }

        /// <summary>
        /// 运行次数
        /// </summary>
        public long RunningTimes { get; set; }

        public string Desc { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
