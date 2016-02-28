using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailProductAttributeMapDto : BaseDto
    {
        public int ProductAttributeID { get; set; }
        public int AttributeID { get; set; }
        public int ProductID { get; set; }
        public bool Filter { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public ProductDetailAttributeDto Attribute { get; set; }
    }
}
