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
    public class QueueMailingListJob : AbstractSharpArchStatefulJob
    {
        private readonly ILog logger;

        public QueueMailingListJob()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public override void ExecuteUnderTransaction(JobExecutionContext context)
        {
            logger.Info("Start QueueMailingListJob....");

            var smtpServer = context.JobDetail.JobDataMap["SmtpServer"] != null ? context.JobDetail.JobDataMap["SmtpServer"].ToString() : string.Empty;
            var fromEmailAddress = context.JobDetail.JobDataMap["FromEmailAddress"] != null ? context.JobDetail.JobDataMap["FromEmailAddress"].ToString() : string.Empty;
            var bccEmailAddress = context.JobDetail.JobDataMap["BccEmailAddress"] != null ? context.JobDetail.JobDataMap["BccEmailAddress"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(fromEmailAddress))
            {
                throw new NullReferenceException("QueueMailingListJob: fromEmailAddress and smtpServer must not be null");
            }

            var mailingListQueueService = (IMailingListQueueService)ServiceLocator.Current.GetService(typeof(IMailingListQueueService));

            if (mailingListQueueService == null)
            {
                throw new NullReferenceException("mailingListQueueService must not be null");
            }

            var mailingListQueues = mailingListQueueService.GetMailingListQueueByStatus(QueueStatusTypeEnum.CREATED);

            using (var workflowRuntime = new WorkflowRuntime())
            {
                workflowRuntime.AddService(new TerminateHelperService());

                using (var manager = new WorkflowRuntimeManager(workflowRuntime))
                {
                    manager.MessageEvent += manager_MessageEvent;

                    for (var i = 0; i < mailingListQueues.Count; i++)
                    {
                        var wfArguments = new Dictionary<string, object> {{"MailingListQueueId", mailingListQueues[i].Id},
                                                                      {"SmtpServer", smtpServer},
                                                                      {"FromEmailAddress", fromEmailAddress},
                                                                      {"BccEmailAddress", bccEmailAddress}};
                        var wrapper = manager.StartWorkflow(typeof(MailingListWorkflow), wfArguments);
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
