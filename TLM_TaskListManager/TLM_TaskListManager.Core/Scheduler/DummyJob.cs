using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLM_TaskListManager.Core.Scheduler
{
    public class DummyJob : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello World!");
        }
    }
}