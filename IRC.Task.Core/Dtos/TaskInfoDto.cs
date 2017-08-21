using IRC.Task.Core.TaskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Dtos
{
    /* ==============================================================================
     * 描述：TaskInfoDto
     * 创建人：李传刚 2017/7/28 13:43:19
     * ==============================================================================
     */
    public class TaskInfoDto : TaskInfo
    {
        public bool UpdateFile { get; set; }

        public bool UpdateConfig { get; set; }
    }
}
