using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLM_TaskListManager.Core.Scheduler
{
    public static class JobExtensions
    {
        public static bool IsJob(Type type)
        {
            return type != null
                && !type.IsAbstract
                && typeof(IJob).IsAssignableFrom(type);
        }
    }
}