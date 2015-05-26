using Castle.Windsor;
using Quartz.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLM_TaskListManager.Core.IOC;
using TLM_TaskListManager.Service;

namespace TLM_TaskListManager.Core.Scheduler
{
    public class QuartzConfigurator
    {
        public QuartzConfigurator()
        {
            //_mappingRegistrar = new MappingRegistrar();
        }

        public void InitializeMappings()
        {
        //    MappingRegistrar.AddMapping();
        }

        public IWindsorContainer InitializeWindsorContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();

            ComponentRegistrar.AddComponentsTo(windsorContainer);

            windsorContainer.RegisterQuartzJobs(typeof(DummyJob).Assembly);
            windsorContainer.RegisterQuartzJob<FileScanJob>();

          //  var wServiceLocator = new WindsorServiceLocatorAdapter(windsorContainer);
           // ServiceLocator.SetLocatorProvider(() => wServiceLocator);

            //var toto = windsorContainer.Resolve<IMailService>();
            //toto.ExecuteMalProcess();

            return windsorContainer;
        }

        public void InitializeNHibernateSession()
        {
            //_sessionManager.NHibernateSessionInitialization();
        }
    }
}
