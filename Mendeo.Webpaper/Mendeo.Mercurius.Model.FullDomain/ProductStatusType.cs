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
    
    public partial class ProductStatusType
    {
        public ProductStatusType()
        {
            this.Product = new HashSet<Product>();
            this.ProductStatus = new HashSet<ProductStatus>();
            this.ProductStatusTypeCultureMap = new HashSet<ProductStatusTypeCultureMap>();
        }
    
        public int ProductStatusTypeID { get; set; }
        public string ProductStatusName { get; set; }
        public string ProductStatusDescription { get; set; }
        public int ProductStatusSequenceNumber { get; set; }
        public bool Activated { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductStatus> ProductStatus { get; set; }
        public virtual ICollection<ProductStatusTypeCultureMap> ProductStatusTypeCultureMap { get; set; }
    }
}