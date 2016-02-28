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
    public class NotifyUserActivity : CodeActivity<bool>
    {
        public InArgument<ProductDetailProductDto> ProductInArgument { get; set; }

        public InArgument<MailerTemplateTypeEnum> MailerTemplateType { get; set; }

        protected override bool Execute(CodeActivityContext context)
        {
            var p = ProductInArgument.Get(context);
            var tt = MailerTemplateType.Get(context);
            var pmqs = context.GetExtension<IProductMailQueueService>();

            if (pmqs == null)
                return false;

            if (p == null)
                return false;

            if (!p.UserID.HasValue) 
                return false;

            pmqs.QueueProductEmail(tt, p.ProductID);
            return true;
        }
    }
}
