using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Common.Data.Infrastructure;
using IRL.Data.AdventureWorks.Infrastructure;
using IRL.Services.Contracts;
using IRL.Services;
using IRL.Data.Repositories.AdventureWorks.Contracts;
using IRL.Data.Repositories.AdventureWorks;
using System.Web.Mvc;
using Elmah.Mvc;
using Common.Web.Mvc.Castle;
using IRL.Data.AdventureWorks;
using IRL.Data.Security;
using IRL.Data.Security.Infrastructure;
using IRL.Mvc.Security;
using IRL.Model.Security;
using IRL.Controllers;
using Common.Web.Mvc.Castle.Contract;

namespace IRL.Web.App_Start
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddDatabaseFactoryTo(container);
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddUnitOfWorkTo(container);
            AddServicesTo(container);
            AddControllersTo(container);
            AddElmahControllersTo(container);
            AddGlobalFilterTo(container);
            AddCustomToolTo(container);
        }

        public static void AddDatabaseFactoryTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IDatabaseFactory<AdventureWorks2012Entities>))
                    .ImplementedBy<AdventureWorksDatabaseFactory>()
                    .IsDefault()
                    .LifestylePerWebRequest()
                    .Named("adventureWorksDatabaseFactory"));

            container.Register(
                Component.For(typeof(IDatabaseFactory<IrlSecurityEntities>))
                    .ImplementedBy<SecurityDatabaseFactory>()
                    .IsDefault()
                    .LifestylePerWebRequest()
                    .Named("securityDatabaseFactory"));
        }

        public static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IAdventureWorksRepository<>))
                    .ImplementedBy(typeof(AdventureWorksRepositoryBase<>))
                    .LifestyleTransient()
                    .Named("adventureWorksGenericRepositories"));

            container.Register(
                Component.For(typeof(ISecurityRepository<>))
                    .ImplementedBy(typeof(SecurityRepositoryBase<>))
                    .LifestyleTransient()
                    .Named("securityGenericRepositories"));
        }

        public static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(Component.For<IEmployeeRepository>()
                                        .ImplementedBy<EmployeeRepository>()
                                        .LifestyleTransient());
        }

        public static void AddUnitOfWorkTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IAdventureWorksUnitOfWork))
                    .ImplementedBy<AdventureWorksUnitOfWorkBase>()
                    .IsDefault()
                    .LifestyleTransient()
                    .Named("adventureWorksUnitOfWork"));

            container.Register(
                Component.For(typeof(ISecurityUnitOfWork))
                    .ImplementedBy<SecurityUnitOfWorkBase>()
                    .IsDefault()
                    .LifestyleTransient()
                    .Named("securityUnitOfWork"));
        }

        public static void AddServicesTo(IWindsorContainer container)
        {
            container.Register(Component.For<IEmployeeService>()
                                        .ImplementedBy<EmployeeService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IRoleService>()
                                        .ImplementedBy<RoleService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IUserService>()
                                        .ImplementedBy<UserService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IAuthorizationService>()
                                        .ImplementedBy<AuthorizationService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IUserRoleService>()
                                        .ImplementedBy<UserRoleService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IWebsitePermissionService>()
                                        .ImplementedBy<WebsitePermissionService>()
                                        .LifestyleTransient());
        }

        public static void AddControllersTo(IWindsorContainer container)
        {
            container.RegisterControllers(typeof(HomeController).Assembly);
        }

        public static void AddElmahControllersTo(IWindsorContainer container)
        {
            container.Register(Classes.FromAssemblyContaining<ElmahController>()
                                        .BasedOn<IController>()
                                        .LifestyleTransient());
        }

        public static void AddGlobalFilterTo(IWindsorContainer container)
        {
            container.Register(Component.For<IAuthorizationFilter>()
                                        .ImplementedBy<IrlAuthorizationFilter>()
                                        .LifestyleTransient());
        }

        public static void AddCustomToolTo(IWindsorContainer container)
        {
            container.Register(Component.For<IWindsorEnumeratorTool>()
                                        .ImplementedBy<WindsorEnumeratorTool>()
                                        .LifestyleTransient());
        }
    }
}