using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Common.Web.Mvc.Castle;
using Elmah.Contrib.WebApi;
using Elmah.Mvc;
using Mendeo.Common.WebApi;
using Mendeo.Mercurius.AdminBootstrap;
using Mendeo.Mercurius.AdminBootstrap.Init;
using Mendeo.Mercurius.AdminController;

namespace Mendeo.Mercurius.AdminWeb
{
    public class WebApiApplication : System.Web.HttpApplication
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
            //GlobalConfiguration.Configuration.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new RiaApiFilterProvider(windsorContainer));
        }

        protected virtual IWindsorContainer InitializeWindsorCastle(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            container.RegisterControllers(typeof(ElmahController).Assembly);
            container.RegisterApiControllers(typeof(AdminWebApi.ProductController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            return container;
        }

        protected virtual void InitializeAutoMapper()
        {
            MappingRegistrar.AddMapping();
        }
    }
}
