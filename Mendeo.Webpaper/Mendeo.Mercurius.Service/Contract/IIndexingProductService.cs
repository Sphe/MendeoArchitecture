using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IIndexingProductService
    {
        string IndexSingleProduct(int productId);
        void UnindexSingleProduct(int productId);
        void IndexProduct();
    }
}
