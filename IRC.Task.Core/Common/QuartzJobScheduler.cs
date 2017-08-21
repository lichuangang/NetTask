using QCommon.Components;
using QCommon.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Common
{
    /* ==============================================================================
     * 描述：QuartzJobScheduler
     * 创建人：李传刚 2017/7/27 9:59:22
     * ==============================================================================
     */
    public class QuartzJobScheduler
    {
        #region 字段

        private ISchedulerFactory scheFactory;
        private static QuartzJobScheduler instance = null;
        private IScheduler scheduler;
        private static object objLock = new object();
        ILogger _logger;
        /// <summary>
        /// 工作组 CronJobGrp
        /// </summary>
        private const string JOB_GROUP = "CronJobGrp";
        #endregion

        #region 单实例
        private QuartzJobScheduler()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(QuartzJobScheduler).FullName);
            scheFactory = new StdSchedulerFactory();
            scheduler = scheFactory.GetScheduler();
            scheduler.Start();
        }

        public static QuartzJobScheduler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        instance = new QuartzJobScheduler();
                    }
                }

                return instance;
            }
        }
        #endregion

        #region 增加job
        /// <summary>
        /// 增加job
        /// </summary>
        /// <typeparam name="T">要执行的job，必需实现IJob接口</typeparam>
        /// <param name="taskName">任务名称不能重复</param>
        /// <param name="cronExpression">cron表达式</param>

        public bool AddJob<T>(string taskName, string cronExpression) where T : IJob
        {
            try
            {
                if (!ExistsJob(taskName))
                {
                    IJobDetail job = JobBuilder.Create<T>().WithIdentity(taskName, JOB_GROUP).Build();
                    DateTimeOffset startRunTime = DateBuilder.NextGivenSecondDate(DateTime.Now, 0);
                    //100年，表示不结束
                    DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddYears(100), 1);
                    ITrigger trigger = TriggerBuilder.Create().
                        StartAt(startRunTime).
                        EndAt(endRunTime).
                        WithIdentity(job.Key.Name, job.Key.Group).
                        WithCronSchedule(cronExpression).Build();

                    scheduler.ScheduleJob(job, trigger);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QuartzJobScheduler", ex);
            }
            return false;
        }
        #endregion

        #region 是否存在job
        /// <summary>
        /// 判断是否存在某个任务
        /// </summary>
        public bool ExistsJob(string taskName)
        {
            return scheduler.CheckExists(new JobKey(taskName, JOB_GROUP));
        }
        #endregion

        #region 删除job
        /// <summary>
        /// 移除业务任务Job
        /// </summary>
        public bool DeleteJob(string taskName)
        {
            if (ExistsJob(taskName))
            {
                return scheduler.DeleteJob(new JobKey(taskName, JOB_GROUP));
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
