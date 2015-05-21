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
    public class QueueMailAccountJob : AbstractSharpArchStatefulJob
    {
        private readonly ILog logger;

        public QueueMailAccountJob()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public override void ExecuteUnderTransaction(JobExecutionContext context)
        {
            logger.Info("Start QueueMailAccountJob....");

            var smtpServer = context.JobDetail.JobDataMap["SmtpServer"] != null ? context.JobDetail.JobDataMap["SmtpServer"].ToString() : string.Empty;
            var fromEmailAddress = context.JobDetail.JobDataMap["FromEmailAddress"] != null ? context.JobDetail.JobDataMap["FromEmailAddress"].ToString() : string.Empty;
            var bccEmailAddress = context.JobDetail.JobDataMap["BccEmailAddress"] != null ? context.JobDetail.JobDataMap["BccEmailAddress"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(fromEmailAddress))
            {
                throw new NullReferenceException("QueueMailAccountJob: fromEmailAddress and smtpServer must not be null");
            }

            var accountQueueMailService = (IAccountQueueMailService)ServiceLocator.Current.GetService(typeof(IAccountQueueMailService));

            if (accountQueueMailService == null)
            {
                throw new NullReferenceException("accountQueueMailService must not be null");
            }

            var accountMailQueues = accountQueueMailService.GetAccountMailQueueByStatus(QueueStatusTypeEnum.CREATED);

            using (var workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.AddService(new TerminateHelperService());

                using (var manager = new WorkflowRuntimeManager(workflowRuntime))
                {
                    manager.MessageEvent += manager_MessageEvent;

                    for (var i = 0; i < accountMailQueues.Count; i++)
                    {
                        var wfArguments = new Dictionary<string, object> {{"AccountMailQueueId", accountMailQueues[i].Id},
                                                                      {"SmtpServer", smtpServer},
                                                                      {"FromEmailAddress", fromEmailAddress},
                                                                      {"BccEmailAddress", bccEmailAddress}};
                        var wrapper = manager.StartWorkflow(typeof(MailAccountWorkflow), wfArguments);
                        var waitTest = manager.WaitOne(wrapper.Id, 30000);

                        if (wrapper.Exception != null)
                        {
                            logger.Error(wrapper.Exception.Message);
                        }
                    }

                    manager.WaitAll(300000);
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
