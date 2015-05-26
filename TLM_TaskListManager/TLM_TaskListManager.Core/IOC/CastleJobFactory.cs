using Castle.Windsor;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLM_TaskListManager.Core.IOC
{
    public class CastleJobFactory : IJobFactory
    {
        private IWindsorContainer container;

        public CastleJobFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return container.Resolve(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}