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
    
    public partial class ProductStatusReason
    {
        public ProductStatusReason()
        {
            this.ProductStatusReasonCultureMap = new HashSet<ProductStatusReasonCultureMap>();
            this.ProductStatusReasonEntry = new HashSet<ProductStatusReasonEntry>();
            this.ProductStatusReasonLink = new HashSet<ProductStatusReasonLink>();
        }
    
        public int ProductStatusReasonID { get; set; }
        public string ProductStatusReasonName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<ProductStatusReasonCultureMap> ProductStatusReasonCultureMap { get; set; }
        public virtual ICollection<ProductStatusReasonEntry> ProductStatusReasonEntry { get; set; }
        public virtual ICollection<ProductStatusReasonLink> ProductStatusReasonLink { get; set; }
    }
}
