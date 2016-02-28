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
    
    public partial class Attribute
    {
        public Attribute()
        {
            this.AttributeCategoryMap = new HashSet<AttributeCategoryMap>();
            this.AttributeEvent = new HashSet<AttributeEvent>();
            this.ProductAttributeMap = new HashSet<ProductAttributeMap>();
        }
    
        public int AttributeID { get; set; }
        public int AttributeTypeID { get; set; }
        public int AttributeItemID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual AttributeItem AttributeItem { get; set; }
        public virtual AttributeType AttributeType { get; set; }
        public virtual ICollection<AttributeCategoryMap> AttributeCategoryMap { get; set; }
        public virtual ICollection<AttributeEvent> AttributeEvent { get; set; }
        public virtual ICollection<ProductAttributeMap> ProductAttributeMap { get; set; }
    }
}
