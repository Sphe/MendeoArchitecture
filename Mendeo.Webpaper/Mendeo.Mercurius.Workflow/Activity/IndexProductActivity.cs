using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Service.Contract;

namespace Mendeo.Mercurius.Workflow.Activity
{
    public class IndexProductActivity : CodeActivity<bool>
    {
        public InArgument<ProductDetailProductDto> ProductInArgument { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            var p = ProductInArgument.Get(context);
            var indexingProductService = context.GetExtension<IIndexingProductService>();

            if (indexingProductService == null)
                return false;

            if (p == null)
                return false;

            indexingProductService.IndexSingleProduct(p.ProductID);
            return true;
        }
    }
}
