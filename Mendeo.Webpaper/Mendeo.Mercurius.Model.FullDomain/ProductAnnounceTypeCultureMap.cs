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
    
    public partial class ProductAnnounceTypeCultureMap
    {
        public int ProductAnnounceTypeCultureID { get; set; }
        public Nullable<int> ProductAnnounceTypeID { get; set; }
        public Nullable<int> CultureID { get; set; }
        public string ProductAnnounceTypeDisplayName { get; set; }
        public string ProductAnnounceTypeDisplayDescription { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual ProductAnnounceType ProductAnnounceType { get; set; }
    }
}
