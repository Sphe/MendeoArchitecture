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
    
    public partial class ProductStatusReasonEntryMap
    {
        public int ProductStatusReasonEntryID { get; set; }
        public int ProductStatusID { get; set; }
        public int ProductStatusReasonCategoryReasonMapID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual ProductStatusReasonEntry ProductStatusReasonEntry { get; set; }
    }
}