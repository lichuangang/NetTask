using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core
{
    /* ==============================================================================
     * 描述：ConfigSettings
     * 创建人：李传刚 2017/7/25 13:36:08
     * ==============================================================================
     */
    public class ConfigSettings
    {
        static ConfigSettings()
        {
            TaskDbConnectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            TaskHostFile = ConfigurationManager.AppSettings["TaskHostFile"];
            DllRootPath = ConfigurationManager.AppSettings["DllRootPath"];
        }

        public static string TaskDbConnectionString { get; private set; }

        public static string TaskHostFile { get; private set; }

        public static string DllRootPath { get; private set; }

    }
}
