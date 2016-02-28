using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailCountryDto
    {
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

        public string CurrencyLabel
        {
            get
            {
                return Currency.Trim();
            }
        }

        public string CurrencyCodeLabel
        {
            get
            {
                return CurrencyCode.Trim();
            }
        }
    }
}
