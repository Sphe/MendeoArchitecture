using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto
{
    public class ProductDetailProductCultureMapDto : BaseDto
    {
        public int ProductCultureID { get; set; }
        public int ProductID { get; set; }
        public int CultureID { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal BaseUnitPrice { get; set; }
        public Nullable<decimal> ExtraShipFee { get; set; }
        public string ProductName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string Keywords { get; set; }
        public bool Activate { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
