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
    
    public partial class GlobalSetting
    {
        public int GlobalSettingID { get; set; }
        public int GlobalSettingTypeID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual GlobalSettingType GlobalSettingType { get; set; }
    }
}