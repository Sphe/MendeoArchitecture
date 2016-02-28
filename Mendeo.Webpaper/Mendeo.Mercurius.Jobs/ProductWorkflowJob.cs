using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core;
using Common.Logging;
using Mendeo.Mercurius.Workflow;
using Mendeo.Mercurius.Workflow.Contract;
using Quartz;
using Mendeo.Mercurius.Service.Contract;

namespace Mendeo.Mercurius.Jobs
{
    [DisallowConcurrentExecution]
    public class ProductWorkflowJob : IJob
    {
        private readonly ILog logger;
        private readonly IProductService _productService;
        private readonly IWorkflowService _workflowService;
        private readonly IDbContextService _dbContextService;

        public ProductWorkflowJob(IWorkflowService workflowService, IDbContextService dbContextService, IProductService productService)
        {
            logger = LogManager.GetLogger(GetType());

            _workflowService = workflowService;
            _dbContextService = dbContextService;
            _productService = productService;
        }

        public virtual async void Execute(IJobExecutionContext context)
        {
            logger.Info("Start Product Workflow....");

            _dbContextService.RefreshFullDomain();

            var lstProduct = _productService.GetAllProductsWorkflowScope();

            lstProduct.ForEachIndexed((x, i) => _workflowService.ExecuteProductMainWorkflow(x.ProductID));

            logger.Info("Finishing Product Workflow....");
        }
    }
}
