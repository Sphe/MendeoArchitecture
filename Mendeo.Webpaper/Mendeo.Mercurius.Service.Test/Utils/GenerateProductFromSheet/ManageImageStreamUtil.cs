using Castle.Windsor;
using Mendeo.Common.Core;
using Mendeo.Mercurius.Bootstrap.Init;
using Mendeo.Mercurius.Service.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test.Utils.GenerateProductFromSheet
{
    class ManageImageStreamUtil
    {

        IProductImageService _productImageService;
        protected IWindsorContainer _container; 

        public ManageImageStreamUtil()
        {
            _container = new WindsorContainer();
            ComponentRegistrar.AddServicesTo(_container);
            ComponentRegistrar.AddNestConnection(_container);
            ComponentRegistrar.AddGenericRepositoriesTo(_container);
            ComponentRegistrar.AddCustomRepositoriesTo(_container);
            ComponentRegistrar.AddUnitOfWorkTo(_container);
            ComponentRegistrar.AddDatabaseFactoryPerThreadTo(_container);
            _productImageService = _container.Resolve<IProductImageService>();
        }

        public bool LoadImage(int productId, int sort, string pFilePath)
        {
            // byte[] imageData,
            Stream fileStream = new FileStream(pFilePath, FileMode.Open);

            _productImageService.ProcessImageProduct(Convert.ToInt32(productId), StreamOperations.ConvertStreamToByteArray(fileStream), sort);
            fileStream.Close();
            return true;
        }
        
    }
}
