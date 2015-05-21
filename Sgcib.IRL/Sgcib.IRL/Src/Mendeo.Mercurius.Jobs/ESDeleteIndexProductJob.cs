using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;
using Mendeo.Mercurius.Service.Contract;

namespace Mendeo.Mercurius.Jobs
{
    [DisallowConcurrentExecution]
    public class ESDeleteIndexProductJob : IJob
    {
        private readonly ILog logger;
        private readonly IIndexingProductService _indexingProductService;
        private readonly IDbContextService _dbContextService;

        public ESDeleteIndexProductJob(IIndexingProductService indexingProductService, IDbContextService dbContextService)
        {
            logger = LogManager.GetLogger(GetType());

            _indexingProductService = indexingProductService;
            _dbContextService = dbContextService;
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            logger.Info("Start Elasticsearch Indexing Product....");

            _dbContextService.RefreshFullDomain();
            _indexingProductService.IndexProduct();

            logger.Info("Finishing Elasticsearch Indexing Product....");
        }
    }
}
