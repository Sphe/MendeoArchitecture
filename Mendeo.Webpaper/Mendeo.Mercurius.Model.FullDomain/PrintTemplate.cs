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
    
    public partial class PrintTemplate
    {
        public int PrintTemplateID { get; set; }
        public Nullable<int> PrintTypeID { get; set; }
        public Nullable<int> CultureID { get; set; }
        public byte[] Body { get; set; }
        public string DisplayName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual PrintTemplateType PrintTemplateType { get; set; }
    }
}
