using System;
using System.Collections.Generic;
using System.Workflow.Runtime;
using Common.Logging;
using Maceria.Common.Technical.Workflow;
using Maceria.eCommerce.AdminServices.ServiceInterfaces;
using Maceria.eCommerce.Shared;
using Maceria.eCommerce.Workflow;
using Maceria.Scheduler.SharpArchJob;
using Microsoft.Practices.ServiceLocation;
using Quartz;

namespace Maceria.eCommerce.Jobs
{
    public class ValidateOrderJob : AbstractSharpArchStatefulJob
    {
        private readonly ILog logger;

        public ValidateOrderJob()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public override void ExecuteUnderTransaction(JobExecutionContext context)
        {
            logger.Info("Start ValidateOrderJob....");

            var paymentUrlProvider = context.JobDetail.JobDataMap["PaymentUrlProvider"].ToString();
            var backupPaymentUrlProvider = context.JobDetail.JobDataMap["BackupPaymentUrlProvider"].ToString();

            var orderService = (IOrderService)ServiceLocator.Current.GetService(typeof(IOrderService));

            if (orderService == null)
            {
                throw new NullReferenceException("orderService must not be null");
            }

            var orders = orderService.GetOrdersByStatus(OrderStatusEnum.Submitted);

            using (var workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.AddService(new TerminateHelperService());

                using (var manager = new WorkflowRuntimeManager(workflowRuntime))
                {
                    manager.MessageEvent += manager_MessageEvent;

                    for (var i = 0; i < orders.Count; i++)
                    {
                        var wfArguments = new Dictionary<string, object> {{"OrderId", orders[i].Id},
                                                                      {"PaymentProviderUrl", paymentUrlProvider},
                                                                      {"BackupPaymentProviderUrl", backupPaymentUrlProvider}};
                        var wrapper = manager.StartWorkflow(typeof(ValidatingOrderWorkflow), wfArguments);
                        var testWait = manager.WaitOne(wrapper.Id, 30000);

                        if (wrapper.Exception != null)
                        {
                            logger.Error(wrapper.Exception.Message);
                        }
                    }

                    manager.WaitAll(30000);
                    manager.ClearAllWorkflows();
                }
            }
        }

        void manager_MessageEvent(object sender, WorkflowLogEventArgs e)
        {
            logger.Info(e.Message);
        }
    }
}
