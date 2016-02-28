using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailCategoryCultureMapDto : BaseDto
    {
        public int CategoryCultureID { get; set; }
        public int CategoryID { get; set; }
        public int CultureID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAlternativeName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaTitle { get; set; }
    }
}
