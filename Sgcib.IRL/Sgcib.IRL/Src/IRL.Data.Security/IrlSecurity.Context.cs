﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IRL.Data.Security
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using IRL.Model.Security;
    
    public partial class IrlSecurityEntities : DbContext
    {
        public IrlSecurityEntities()
            : base("name=IrlSecurityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<WebSiteAccessPermission> WebSiteAccessPermission { get; set; }
    }
}