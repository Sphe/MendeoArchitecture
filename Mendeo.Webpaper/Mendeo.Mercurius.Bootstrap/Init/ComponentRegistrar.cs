using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Data.Infrastructure;
using Common.Web.Mvc.Castle.Contract;
using EasyNetQ;
using Mendeo.Mercurius.Data.FullDomain;
using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Data.Repository.FullDomain;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service;
using Mendeo.Mercurius.Service.Contract;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Bootstrap.Init
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            //AddDatabaseFactoryTo(container);
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddUnitOfWorkTo(container);
            AddServicesTo(container);
        }

        public static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IMercuritusFullDomainRepository<>))
                    .ImplementedBy(typeof(MercuritusFullDomainRepositoryBase<>))
                    .LifestyleTransient()
                    .Named("fullDomainGenericRepositories"));
        }

        public static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(Component.For<IProductRepository>()
                                        .ImplementedBy<ProductRepository>()
                                        .LifestyleTransient());
        }

        public static void AddUnitOfWorkTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IMercuritusFullDomainUnitOfWork))
                    .ImplementedBy<MercuritusFullDomainUnitOfWorkBase>()
                    .IsDefault()
                    .LifestyleTransient()
                    .Named("fullDomainUnitOfWork"));
        }

        public static void AddServicesTo(IWindsorContainer container)
        {
            container.Register(Component.For<ICultureService>()
                                        .ImplementedBy<CultureService>()
                                        .LifestyleSingleton());

            container.Register(Component.For<IRequestingProductService>()
                                        .ImplementedBy<RequestingProductService>()
                                        .LifestyleTransient());

            container.Register(Component.For<ILocationService>()
                                        .ImplementedBy<LocationService>()
                                        .LifestyleTransient());
        }
    }
}
