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
    
    public partial class ProductFavorite
    {
        public int ProductFavoriteID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
