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
    
    public partial class Country
    {
        public Country()
        {
            this.City = new HashSet<City>();
            this.PostalCode = new HashSet<PostalCode>();
            this.Region = new HashSet<Region>();
        }
    
        public int CountryID { get; set; }
        public Nullable<int> ContinentID { get; set; }
        public Nullable<int> GeoNameID { get; set; }
        public string Country1 { get; set; }
        public string FIPS104 { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
        public string ISON { get; set; }
        public string Internet { get; set; }
        public string Capital { get; set; }
        public string MapReference { get; set; }
        public string NationalitySingular { get; set; }
        public string NationalityPlural { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<int> Population { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<City> City { get; set; }
        public virtual Continent1 Continent1 { get; set; }
        public virtual ICollection<PostalCode> PostalCode { get; set; }
        public virtual ICollection<Region> Region { get; set; }
    }
}
