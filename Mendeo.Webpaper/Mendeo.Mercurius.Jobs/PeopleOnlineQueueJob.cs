using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Quartz;
using Mendeo.Mercurius.Service.Contract;

namespace Mendeo.Mercurius.Jobs
{
    [DisallowConcurrentExecution]
    public class PeopleOnlineQueueJob : IInterruptableJob
    {
        private readonly ILog logger;
        private readonly IPeopleOnlineService _peopleOnlineService;
        private readonly IDbContextService _dbContextService;

        public PeopleOnlineQueueJob(IPeopleOnlineService peopleOnlineService, IDbContextService dbContextService)
        {
            logger = LogManager.GetLogger(GetType());

            _peopleOnlineService = peopleOnlineService;
            _dbContextService = dbContextService;
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            logger.Info("Starting PeopleOnline DeQueue....");

            _dbContextService.RefreshFullDomain();

            _peopleOnlineService.ProcessQueuePeopleOnline("scheduler-job-people-online");

            Thread.Sleep(Int32.MaxValue);

            logger.Info("Finishing PeopleOnline DeQueue....");
        }

        public void Interrupt()
        {
            _peopleOnlineService.DisposeQueuePeopleOnline();
        }
    }
}
