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
    
    public partial class RolePermissionMap
    {
        public int RolePermissionMapID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> RolePermissionID { get; set; }
    
        public virtual RolePermission RolePermission { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}