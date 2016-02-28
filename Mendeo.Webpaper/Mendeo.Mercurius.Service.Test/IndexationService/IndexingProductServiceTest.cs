using Mendeo.Mercurius.Service.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Test.IndexationService
{
    [TestFixture]
    public class IndexingProductServiceTest : BaseWindsorCastleTest
    {
        [Test]
        public void Can_I_Unindex_Product_From_ElasticSearch()
        {
            var indexService = _container.Resolve<IIndexingProductService>();

            var IdsToUnindex = new List<int>() { 130519,
130518,
130517,
130516,
130515,
130514,
130513,
130512,
130511,
130510,
130507,
130506,
130505,
130497,
130496,
130495,
130494,
130493,
130492,
130491 };

            foreach(var id in IdsToUnindex)
            {
                indexService.UnindexSingleProduct(id);
            }
        }


    }
}
