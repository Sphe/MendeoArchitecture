using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Enum;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IProductMailQueueService
    {
        IList<ProductMailQueueDto> GetProductMailQueueCreated();

        void QueueProductEmail(MailerTemplateTypeEnum mailerType, int productId);

        void ProcessQueueProductEmail(string subscriptionId);

        void DisposeQueueProductMail();
    }
}
