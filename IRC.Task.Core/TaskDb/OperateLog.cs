using QCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.TaskDb
{
    /* ==============================================================================
     * 描述：OperateLog
     * 创建人：李传刚 2017/7/25 11:28:39
     * ==============================================================================
     */
    public class OperateLog : IdKey<int>
    {
        /// <summary>
        /// 日志操作类型：1-创建,2-修改,3-立即执行任务,4-停止任务,5-删除
        /// </summary>
        public sbyte OperateType { get; set; }

        /// <summary>
        /// 操作明细
        /// </summary>
        public string OperateDetail { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
