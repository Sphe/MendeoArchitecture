using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Enum;

namespace Mendeo.Mercurius.Workflow.Activity
{
    public class CheckProductStatusActivity : CodeActivity<bool>
    {
        public InArgument<ProductDetailProductDto> ProductInArgument { get; set; }

        public InArgument<ProductStatusTypeEnum> ProductStatusToCheck { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            var p = ProductInArgument.Get(context);

            if (p == null)
            {
                return false;
            }

            var status = ProductStatusToCheck.Get(context);

            return p.ProductLastStatusID == status;
        }
    }
}

