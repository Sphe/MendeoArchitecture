using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TLM_TaskListManager.Core.Scheduler;
using Topshelf;

namespace TLM_TaskListManager.Scheduler.Console
{ /// <summary>
    /// The server's main entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        public static void Main()
        {
            //Install-Package Common.Logging.Log4Net1213 -Version 3.0.0
            //Install-Package log4net
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription(Configuration.ServiceDescription);
                x.SetDisplayName(Configuration.ServiceDisplayName);
                x.SetServiceName(Configuration.ServiceName);

                x.Service(factory =>
                {
                    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                    QuartzServer server = new QuartzServer();
                    server.Initialize();
                    return server;
                });
            });
        }
    }
}