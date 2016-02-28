using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailCategoryProductMapDto : BaseDto
    {
        public int CategoryProductID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }

        public ProductDetailCategoryDto Category { get; set; }
    }
}
