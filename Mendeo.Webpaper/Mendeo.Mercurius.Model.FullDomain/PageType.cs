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
    
    public partial class PageType
    {
        public PageType()
        {
            this.Page = new HashSet<Page>();
        }
    
        public int PageTypeID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Page> Page { get; set; }
    }
}
