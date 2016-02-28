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
    
    public partial class Page
    {
        public Page()
        {
            this.Page1 = new HashSet<Page>();
            this.PageCultureMap = new HashSet<PageCultureMap>();
        }
    
        public int PageID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> PageTypeID { get; set; }
        public Nullable<int> ParentPageID { get; set; }
        public string Title { get; set; }
        public bool Activate { get; set; }
        public bool IsDefault { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Page> Page1 { get; set; }
        public virtual Page Page2 { get; set; }
        public virtual PageType PageType { get; set; }
        public virtual ICollection<PageCultureMap> PageCultureMap { get; set; }
    }
}
