using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace WiseThink.Scheduler
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ScheduledTasks>().Build();

            //Trigger will fire every year in month of April 10 at 10:15 am
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                            .WithIdentity("triggerx1", "groupx1")
                                                            .WithCronSchedule("0 15 10 10 4 ?")
                                                            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
