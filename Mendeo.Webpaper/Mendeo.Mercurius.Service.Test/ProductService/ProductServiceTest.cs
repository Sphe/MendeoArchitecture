using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test
{
    [TestFixture]
    public class ProductServiceTest : BaseWindsorCastleTest
    {
        [Test]
        public void AddProduct()
        {
            var service = _container.Resolve<IRequestingProductService>();

            var request = new RequestIndexedProduct();
            request.Size = Int16.MaxValue;
            request.Filters.Add(new RequestBucketComposite
            {
                Key = "Processor",
                BucketTransformationType = Enums.BucketTransformationEnum.TermType
            });

            var response = service.RequestProductPaginated(request);
        }
    }


}
