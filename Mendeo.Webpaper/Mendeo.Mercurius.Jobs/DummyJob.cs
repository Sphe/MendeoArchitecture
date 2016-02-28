using System;
using Quartz;

namespace Mendeo.Mercurius.Jobs
{
    public class DummyJob : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
