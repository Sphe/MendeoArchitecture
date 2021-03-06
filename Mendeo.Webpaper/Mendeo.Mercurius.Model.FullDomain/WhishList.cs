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
    
    public partial class WhishList
    {
        public WhishList()
        {
            this.WhishListEvent = new HashSet<WhishListEvent>();
            this.WhishListProductMap = new HashSet<WhishListProductMap>();
        }
    
        public int WhishListID { get; set; }
        public System.Guid WhishListGUID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int CultureID { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public bool Activate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Culture Culture { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<WhishListEvent> WhishListEvent { get; set; }
        public virtual ICollection<WhishListProductMap> WhishListProductMap { get; set; }
    }
}
