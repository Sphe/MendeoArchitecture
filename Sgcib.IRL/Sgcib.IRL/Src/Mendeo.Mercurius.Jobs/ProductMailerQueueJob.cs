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
    public class ProductMailerQueueJob : IInterruptableJob
    {
        private readonly ILog logger;
        private readonly IProductMailQueueService _productMailQueueService;
        private readonly IDbContextService _dbContextService;

        public ProductMailerQueueJob(IProductMailQueueService productMailQueueService, IDbContextService dbContextService)
        {
            logger = LogManager.GetLogger(GetType());

            _productMailQueueService = productMailQueueService;
            _dbContextService = dbContextService;
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            logger.Info("Starting Product Mailer DeQueue....");

            _dbContextService.RefreshFullDomain();

            _productMailQueueService.ProcessQueueProductEmail("scheduler-job-every30s");

            Thread.Sleep(Int32.MaxValue);

            logger.Info("Finishing Product Mailer DeQueue....");
        }

        public void Interrupt()
        {
            _productMailQueueService.DisposeQueueProductMail();
        }
    }
}
