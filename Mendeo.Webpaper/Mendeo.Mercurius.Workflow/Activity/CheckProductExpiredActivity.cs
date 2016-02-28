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
    public class CheckProductExpiredActivity : CodeActivity<bool>
    {
        public InArgument<ProductDetailProductDto> ProductInArgument { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            var now = DateTime.Now;
            var p = ProductInArgument.Get(context);

            if (p == null)
                return false;

            var timeSpanExpired = now - p.CreatedOn;

            return timeSpanExpired.TotalDays > 60;
        }
    }
}
