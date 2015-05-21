using System;
using Mendeo.Scheduler.Core;
using Topshelf;
using System.IO;

namespace Mendeo.Scheduler.Console
{
    /// <summary>
    /// The server's main entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        public static void Main()
        {
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
