using Common.Logging;
using Quartz;

namespace Mendeo.Mercurius.Jobs
{
    [DisallowConcurrentExecution]
    public class ESIndexProductJob : IJob
    {
        private readonly ILog logger;
        
        public ESIndexProductJob()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            logger.Info("Start Solr Indexing Product....");
            //_solrService.IndexAllProducts();
            logger.Info("Finishing Solr Indexing Product....");
        }
    }
}
