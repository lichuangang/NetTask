using IRC.Task.Core.Common;
using IRC.Task.Core.Dtos;
using IRC.Task.Repositories;
using QCommon.Components;
using QCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Applications.Impl
{
    /* ==============================================================================
     * 描述：TaskRunService
     * 创建人：李传刚 2017/7/27 18:36:15
     * ==============================================================================
     */
    [Component]
    public class TaskRunService : ITaskRunService
    {
        ITaskInfoRepository _taskInfoRsy = ObjectContainer.Resolve<ITaskInfoRepository>();
        ITaskSettingRepository _settingRsy = ObjectContainer.Resolve<ITaskSettingRepository>();
        TaskProcessPool _taskProcessPoll = TaskProcessPool.Instance;

        private TaskParameter GetParam(int settingId)
        {

            var setting = _settingRsy.GetById(settingId);
            if (setting == null)
            {
                throw new BusinessException("参数异常：设置ID或TaskId无效", "500");
            }
            var taskInfo = _taskInfoRsy.GetById(setting.TaskInfoId);
            if (taskInfo == null)
            {
                throw new BusinessException("参数异常：设置ID或TaskId无效", "500");
            }

            return new TaskParameter
            {
                SettingId = setting.Id,
                TaskType = setting.TaskType,
                Status = setting.Status,
                TaskAssemblyName = taskInfo.TaskAssemblyName,
                TaskRootPath = Path.Combine(ResourceFilePath.TaskRun, taskInfo.TaskName),//@"D:\IRC_ServerSide\trunk\IRC.Task.Svc\TaskDemo1\bin\Debug",
                CronExpression = setting.CronExpression,
                TaskInitParameter = setting.BusinessParameter,
                ThreadCount = setting.ThreadCount
            };
        }

        public void StartTask(int settingId)
        {
            var param = GetParam(settingId);
            if (param.TaskType == 0)
            {
                Run(param);
            }
            else if (param.TaskType == 1)
            {
                if (param.Status == 2)
                {
                    throw new BusinessException("程序正在就绪中，不能重复执行", "500");
                }

                var taskName = string.Format("{0}_{1}", param.TaskAssemblyName, param.SettingId);
                QuartzJobScheduler.Instance.AddJob<QuatztTask>(taskName, param.CronExpression);
                //将状态改为执行中状态改为就绪
                _settingRsy.UpdateStatus(settingId, 2);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        private void Run(TaskParameter param)
        {
            if (param.Status == 1)
            {
                throw new BusinessException("程序正在运行中，不能重复执行", "500");
            }
            _taskProcessPoll.StartTask(param);
            //将状态改为执行中
            _settingRsy.UpdateStatus(param.SettingId, 1);
        }

        /// <summary>
        /// 由定时任务执行
        /// </summary>
        public void QuartzRun(int settingId)
        {
            var param = GetParam(settingId);
            //可能异常产生,造成任务结束.
            if (param.Status == 0)
            {
                var taskName = string.Format("{0}_{1}", param.TaskAssemblyName, param.SettingId);
                QuartzJobScheduler.Instance.DeleteJob(taskName);
                return;
            }

            if (param.Status == 1)
            {
                throw new BusinessException("当前程序正在运行中,或者是时间设置不合理,请查询日志重新设置", "500");
            }
            Run(param);
        }

        #region 停止任务
        public void StopTask(int settingId)
        {
            var param = GetParam(settingId);
            //定时任务停止
            if (param.TaskType == 1)
            {
                var taskName = string.Format("{0}_{1}", param.TaskAssemblyName, param.SettingId);
                QuartzJobScheduler.Instance.DeleteJob(taskName);
            }
            //杀死进程
            _taskProcessPoll.StopTask(settingId);
            //将状态改为停止
            _settingRsy.UpdateStatus(settingId, 0);
        }

        public void RestQuartzTask(int settingId)
        {
            //杀死进程
            _taskProcessPoll.StopTask(settingId);
            //将状态改为就绪
            _settingRsy.UpdateStatus(settingId, 2);
        }
        #endregion

        public void Restart()
        {
            //1.取出所有待执行状态的定时任务
            var settings = _settingRsy.GetListByStatus(1, 2);
            //2.将任务执行停止(保证流程统一)
            settings.ForEach(m => StopTask(m.Id));
            //3.启动任务
            settings.ForEach(m => StartTask(m.Id));
        }
    }
}
