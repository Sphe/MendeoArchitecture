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
    public class QueueMailOrderJob : AbstractSharpArchStatefulJob
    {
        private readonly ILog logger;

        public QueueMailOrderJob()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public override void ExecuteUnderTransaction(JobExecutionContext context)
        {
            logger.Info("Start QueueMailOrderJob....");

            var smtpServer = context.JobDetail.JobDataMap["SmtpServer"] != null ? context.JobDetail.JobDataMap["SmtpServer"].ToString() : string.Empty;
            var fromEmailAddress = context.JobDetail.JobDataMap["FromEmailAddress"] != null ? context.JobDetail.JobDataMap["FromEmailAddress"].ToString() : string.Empty;
            var bccEmailAddress = context.JobDetail.JobDataMap["BccEmailAddress"] != null ? context.JobDetail.JobDataMap["BccEmailAddress"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(fromEmailAddress))
            {
                throw new NullReferenceException("QueueMailOrderJob: fromEmailAddress and smtpServer must not be null");
            }

            var orderQueueMailService = (IOrderQueueMailService)ServiceLocator.Current.GetService(typeof(IOrderQueueMailService));

            if (orderQueueMailService == null)
            {
                throw new NullReferenceException("orderQueueMailService must not be null");
            }

            var orderMailQueues = orderQueueMailService.GetOrderMailQueueByStatus(QueueStatusTypeEnum.CREATED);

            using (var workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.AddService(new TerminateHelperService());

                using (var manager = new WorkflowRuntimeManager(workflowRuntime))
                {
                    manager.MessageEvent += manager_MessageEvent;

                    for (var i = 0; i < orderMailQueues.Count; i++)
                    {
                        var wfArguments = new Dictionary<string, object> {{"OrderMailQueueId", orderMailQueues[i].Id},
                                                                      {"SmtpServer", smtpServer},
                                                                      {"FromEmailAddress", fromEmailAddress},
                                                                      {"BccEmailAddress", bccEmailAddress}};
                        var wrapper = manager.StartWorkflow(typeof(MailOrderWorkflow), wfArguments);
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
