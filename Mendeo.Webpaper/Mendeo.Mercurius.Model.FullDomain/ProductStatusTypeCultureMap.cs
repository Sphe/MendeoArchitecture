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
    
    public partial class ProductStatusTypeCultureMap
    {
        public int ProductStatusTypeCultureMapID { get; set; }
        public int ProductStatusTypeID { get; set; }
        public int CultureID { get; set; }
        public string ProductStatusDisplayName { get; set; }
        public string ProductStatusDisplayDescription { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual ProductStatusType ProductStatusType { get; set; }
    }
}
