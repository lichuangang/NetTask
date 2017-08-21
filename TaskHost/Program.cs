using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common.Dao;
using Task.Common.Utilities;
using TaskHost.Dtos;
using TaskHost.Utils;

namespace TaskHost
{
    class Program
    {
        static ILog _logger = Log4NetLoggerFactory.Instance.Create("TaskHost.RunTask");
        static void Main(string[] args)
        {
            TaskParameter param = null;
            try
            {
                param = MainParamUtils.Deserialize(args);
                _logger.InfoFormat("开始执行任务:{0}", param.TaskAssemblyName);
                Invoke(param);
                _logger.InfoFormat("执行任务结束:{0}", param.TaskAssemblyName);

                if (param.TaskType == 1)
                {
                    //定时任务状态改为就绪
                    TaskDao.SetTaskStatus(param.SettingId, 2);
                }
                else
                {
                    TaskDao.SetTaskStatus(param.SettingId, 0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("执行异常Param:" + string.Join(",", args), ex);
                if (param != null)
                {
                    try
                    {
                        if (param.TaskType == 1)
                        {
                            //出现异常了,把任务置为停止状态,并记录异常
                            TaskDao.SetTaskStatus(param.SettingId, 2, ex.ToString());
                        }
                        else
                        {
                            //出现异常了,把任务置为停止状态,并记录异常
                            TaskDao.SetTaskStatus(param.SettingId, 0, ex.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error("执行异常", e);
                    }
                }
            }
            finally
            {
                System.Environment.Exit(0);
            }
        }

        private static void Invoke(TaskParameter param)
        {
            new TaskContainer(param).StartTask();
        }
    }
}
