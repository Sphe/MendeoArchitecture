using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Web.Mvc.Castle;
using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Bootstrap;
using Mendeo.Mercurius.Controllers;
using Mendeo.Mercurius.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mendeo.Mercurius.Bootstrap.Init;
using Elmah.Contrib.WebApi;
using Elmah.Mvc;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Mercurius.WebApi.PeopleOnline;

namespace Mendeo.Mercurius.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            IWindsorContainer windsorContainer = new WindsorContainer();
            //windsorContainer = InitializeSolr(windsorContainer);
            windsorContainer = InitializeWindsorCastle(windsorContainer);
            InitializeAutoMapper();

            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            //FilterProviders.Providers.Add(new RiaFilterProvider(windsorContainer));
            //GlobalConfiguration.Configuration.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new DeoApiFilterProvider(windsorContainer));
        }

        protected virtual IWindsorContainer InitializeWindsorCastle(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            container.RegisterControllers(typeof(ElmahController).Assembly);
            container.RegisterApiControllers(typeof(ProductController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            //container.Register(
            //    Component.For<IApiMendeoFilter>().ImplementedBy<PeopleOnlineApiFilter>().LifestyleTransient());

            return container;
        }

        protected virtual void InitializeAutoMapper()
        {
            MappingRegistrar.AddMapping();
        }
    }
}
