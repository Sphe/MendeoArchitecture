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
    
    public partial class CategoryCultureMap
    {
        public int CategoryCultureID { get; set; }
        public int CategoryID { get; set; }
        public int CultureID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAlternativeName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTitle { get; set; }
        public string IconCssClass { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Culture Culture { get; set; }
    }
}