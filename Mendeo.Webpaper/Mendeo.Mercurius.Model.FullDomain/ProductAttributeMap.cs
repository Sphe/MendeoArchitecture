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
    
    public partial class ProductAttributeMap
    {
        public int ProductAttributeID { get; set; }
        public int AttributeID { get; set; }
        public int ProductID { get; set; }
        public bool Filter { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Product Product { get; set; }
    }
}