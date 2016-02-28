using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto
{
    public class ProductMailQueueDto
    {
        public int ProductMailQueueID { get; set; }
        public int QueueStatusID { get; set; }
        public int ProductID { get; set; }
        public int MailerTemplateID { get; set; }
        public bool Activate { get; set; }
        public string Message { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}
