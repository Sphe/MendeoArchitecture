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
    public class ChangeProductStatusActivity : CodeActivity<bool>
    {
        public InArgument<ProductDetailProductDto> ProductInArgument { get; set; }

        public InArgument<ProductStatusTypeEnum> NewProductStatus { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            var p = ProductInArgument.Get(context);

            if (p == null)
            {
                return false;
            }

            var status = NewProductStatus.Get(context);

            var ps = context.GetExtension<IProductService>();

            if (ps == null)
            {
                return false;
            }

            var res = ps.ChangeProductStatus(p.ProductID, status);
            return res.Count == 0;
        }
    }
}
