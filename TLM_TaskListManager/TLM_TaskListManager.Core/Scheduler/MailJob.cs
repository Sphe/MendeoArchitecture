using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using TLM_TaskListManager.Service;

namespace TLM_TaskListManager.Core.Scheduler
{
    public class MailJob : IJob
    {
        private readonly IMailService mailService;

        public MailJob(IMailService mailService)
        {
            this.mailService = mailService;
        }



        public void Execute(IJobExecutionContext context)
        {
            mailService.ExecuteMalProcess();
        }
    }
}
