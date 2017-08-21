using Quartz;
using Quartz.Impl;

namespace TagBooksTask
{
    public static class TaskScheduler
    {
        private static IScheduler _scheduler;

        private static void Init()
        {
            AddTask<TagBookJob>("0 0/1 * * * ?  ");
            //AddTask<TimerTask>("0/3 * * * * ? ");

            //AddTask<HideCommentTask>("0/5 * * * * ? ");

            //每分钟执行一次
            //AddTask<TagBookBeginTask>("0 0/1 * * * ? ");
            //AddTask<TagBookEndTask>("0 0/1 * * * ? ");
        }

        private static void AddTask<T>(string cron, bool startNow = false) where T : IJob
        {
            var type = typeof(T);
            var job = JobBuilder.Create(type).Build();
            var triggerBuilder = TriggerBuilder.Create()
              .WithIdentity(type.Name, "tasks")
              .WithCronSchedule(cron);
            if (startNow)
            {
                triggerBuilder.StartNow();
            }
            var trigger = triggerBuilder.Build();
            _scheduler.ScheduleJob(job, trigger);
        }

        public static void Start()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            Init();
            _scheduler.Start();
        }


    }
}
