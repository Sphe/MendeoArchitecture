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
    
    public partial class ProductAnnounceType
    {
        public ProductAnnounceType()
        {
            this.Product = new HashSet<Product>();
            this.ProductAnnounceTypeCultureMap = new HashSet<ProductAnnounceTypeCultureMap>();
        }
    
        public int ProductAnnounceTypeID { get; set; }
        public string ProductAnnounceTypeCodeName { get; set; }
        public string ProductAnnounceTypeDescription { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductAnnounceTypeCultureMap> ProductAnnounceTypeCultureMap { get; set; }
    }
}
