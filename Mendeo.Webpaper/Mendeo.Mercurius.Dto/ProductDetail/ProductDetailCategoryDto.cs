using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailCategoryDto : BaseDto
    {
        public int CategoryID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public bool IsDefault { get; set; }
        public bool Activate { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        public ICollection<ProductDetailCategoryCultureMapDto> CategoryCultureMap { get; set; }
    }
}
