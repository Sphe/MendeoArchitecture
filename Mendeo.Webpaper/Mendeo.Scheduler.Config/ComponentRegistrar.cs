using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Data.Infrastructure;
using EasyNetQ;
using Mendeo.Mercurius.Data.FullDomain;
using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Data.Repository.FullDomain;
using Mendeo.Mercurius.Service;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Mercurius.Workflow;
using Mendeo.Mercurius.Workflow.Contract;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Scheduler.Config
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
            AddNestConnection(container);
            AddWorflowService(container);
            AddRabbitMqConnection(container);
        }

        private static void AddWorflowService(IWindsorContainer container)
        {
            container.Register(Component.For<IWorkflowService>()
                                        .ImplementedBy<WorkflowService>()
                                        .LifestyleTransient());
        }

        public static void AddDatabaseFactoryTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IDatabaseFactory<MercuriusEntities>))
                    .ImplementedBy<MercuritusFullDomainDatabaseFactory>()
                    .IsDefault()
                    .LifestylePerThread()
                    .Named("fullDomainDatabaseFactory"));
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
            container.Register(Component.For<IProductService>()
                                        .ImplementedBy<ProductService>()
                                        .LifestyleTransient());

            container.Register(Component.For<ICultureService>()
                                        .ImplementedBy<CultureService>()
                                        .LifestyleSingleton());

            container.Register(Component.For<IAttributeService>()
                                        .ImplementedBy<AttributeService>()
                                        .LifestyleTransient());

            container.Register(Component.For<ICategoryService>()
                                        .ImplementedBy<CategoryService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IRequestingProductService>()
                                        .ImplementedBy<RequestingProductService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IProductImageService>()
                                        .ImplementedBy<ProductImageService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IGlobalSettingService>()
                                        .ImplementedBy<GlobalSettingService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IIndexingProductService>()
                                        .ImplementedBy<IndexingProductService>()
                                        .LifestyleTransient());

            container.Register(Component.For<ILocationService>()
                                        .ImplementedBy<LocationService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IUserService>()
                                        .ImplementedBy<UserService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IProductMailQueueService>()
                                        .ImplementedBy<ProductMailQueueService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IPeopleOnlineService>()
                                        .ImplementedBy<PeopleOnlineService>()
                                        .LifestyleTransient());

            container.Register(Component.For<IDbContextService>()
                            .ImplementedBy<DbContextService>()
                            .LifestyleTransient());
        }

        public static void AddNestConnection(IWindsorContainer container)
        {
            container.Register(Component.For<IElasticClient>()
                .UsingFactoryMethod(() =>
                {
                    var setting = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["elasticSearchUrl"]));
                    setting.SetDefaultIndex("mercurius-product");
                    var client = new ElasticClient(setting);
                    return client;
                }, managedExternally: true)
                    .LifeStyle.Is(LifestyleType.Singleton));
        }

        private static IBus CreateMessageBus()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["easynetq"];
            return RabbitHutch.CreateBus(connectionString.ConnectionString);
        }

        private static void AddRabbitMqConnection(IWindsorContainer container)
        {
            container.Register(
                Component.For<IBus>().UsingFactoryMethod(CreateMessageBus).LifestyleSingleton()
            );
        }
    }
}
