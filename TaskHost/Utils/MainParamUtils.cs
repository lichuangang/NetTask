using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHost.Dtos;

namespace TaskHost.Utils
{
    /* ==============================================================================
     * 描述：MainParamUtils
     * 创建人：李传刚 2017/8/1 13:00:52
     * ==============================================================================
     */
    public class MainParamUtils
    {
        /// <summary>
        /// 还原TaskParameter参数
        /// </summary>
        public static TaskParameter Deserialize(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return new TaskParameter { SettingId = 11, TaskAssemblyName = "TagBookBeginTask.dll", TaskRootPath = @"D:\IRC_ServerSide\trunk\IRC.Task.Svc\TagBookBeginTask\bin\Debug" };
            }

            //格式为：{json:{实际内容}}
            var argsStr = string.Join("", args);
            argsStr = argsStr.Substring(6);
            argsStr = argsStr.Substring(0, argsStr.Length - 1);
            return JsonConvert.DeserializeObject<TaskParameter>(argsStr);
        }
    }
}
