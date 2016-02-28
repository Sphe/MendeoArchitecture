using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Model.FullDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Data.Repository.FullDomain
{
    public interface IProductRepository : IMercuritusFullDomainRepository<Product>
    {
        IQueryable<IndexedProduct> GetProductToBeIndexed(int currentCultureId);

        IndexedProduct GetProductToBeIndexed(int productId, int currentCultureId);
    }
}
