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
    
    public partial class PageCultureMap
    {
        public PageCultureMap()
        {
            this.PageEvent = new HashSet<PageEvent>();
        }
    
        public int PageCultureMapID { get; set; }
        public Nullable<int> PageID { get; set; }
        public Nullable<int> CultureID { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string Keywords { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual Page Page { get; set; }
        public virtual ICollection<PageEvent> PageEvent { get; set; }
    }
}
