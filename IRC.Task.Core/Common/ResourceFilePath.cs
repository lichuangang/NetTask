using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Common
{
    /* ==============================================================================
     * 描述：ResourceFilePath
     * 创建人：李传刚 2017/7/28 10:58:34
     * ==============================================================================
     */
    public class ResourceFilePath
    {
        static string _basePath = ConfigSettings.DllRootPath;

        /// <summary>
        /// 临时文件目录
        /// </summary>
        public static string TempPath
        {
            get
            {
                return Path.Combine(_basePath, "Task");
            }
        }

        public static string TaskRun
        {
            get
            {
                return Path.Combine(_basePath, "TaskRun");
            }
        }
    }
}
