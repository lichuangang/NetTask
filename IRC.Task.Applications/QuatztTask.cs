using QCommon.Components;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Applications
{
    /* ==============================================================================
     * 描述：QuatztTask
     * 创建人：李传刚 2017/7/27 18:46:08
     * ==============================================================================
     */
    public class QuatztTask : IJob
    {

        ITaskRunService _taskRunService = ObjectContainer.Resolve<ITaskRunService>();

        public void Execute(IJobExecutionContext context)
        {
            var settingId = int.Parse(context.Trigger.Key.Name.Split('_')[1]);
            _taskRunService.QuartzRun(settingId);
        }
    }
}
