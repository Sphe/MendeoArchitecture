using System.Activities;
using System.Threading;
using Mendeo.Common.Core;
using Mendeo.Mercurius.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Workflow.Contract;

namespace Mendeo.Mercurius.Workflow
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IProductService _productService;
        private readonly IProductMailQueueService _productMailQueueService;
        private readonly IIndexingProductService _indexingProductService;

        public WorkflowService(IProductService productService, IProductMailQueueService productMailQueueService,
            IIndexingProductService indexingProductService)
        {
            _productService = productService;
            _productMailQueueService = productMailQueueService;
            _indexingProductService = indexingProductService;
        }

        public void ExecuteProductMainWorkflow(int productId)
        {
            Check.Require(productId > 0, "productId must be valid");
            var task = _productService.GetProductDetailById(productId);
            Task.WaitAll(task);

            var dicoInput = new Dictionary<string, object>()
            {
                {"productDto", task.Result}
            };

            var app = new WorkflowApplication(new ProductMainWorkflow(), dicoInput);

            app.SynchronizationContext = new SynchronousSynchronizationContext();

            app.Extensions.Add(_productService);
            app.Extensions.Add(_productMailQueueService);
            app.Extensions.Add(_indexingProductService);
            
            app.Run();
        }
    }
}
