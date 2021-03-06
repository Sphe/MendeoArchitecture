//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mendeo.Mercurius.Model.FullDomain
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMailQueue
    {
        public int ProductMailQueueID { get; set; }
        public int QueueStatusID { get; set; }
        public int ProductID { get; set; }
        public int MailerTemplateID { get; set; }
        public bool Activate { get; set; }
        public string Message { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual MailerTemplate MailerTemplate { get; set; }
        public virtual Product Product { get; set; }
        public virtual QueueStatus QueueStatus { get; set; }
    }
}
