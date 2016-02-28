using Castle.Windsor;
using Mendeo.Mercurius.Bootstrap.Init;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test
{
    public class BaseWindsorCastleTest
    {
        protected IWindsorContainer _container; 

        [SetUp]
        public void SetUp()
        {
            _container = new WindsorContainer();
            ComponentRegistrar.AddServicesTo(_container);
            ComponentRegistrar.AddNestConnection(_container);
            ComponentRegistrar.AddGenericRepositoriesTo(_container);
            ComponentRegistrar.AddCustomRepositoriesTo(_container);
            ComponentRegistrar.AddUnitOfWorkTo(_container);
            ComponentRegistrar.AddDatabaseFactoryPerThreadTo(_container);
            SetuppingTest();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        public virtual void SetuppingTest()
        {

        }
    }
}
