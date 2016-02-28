using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailAttributeDto : BaseDto
    {
        public int AttributeID { get; set; }
        public int AttributeTypeID { get; set; }
        public int AttributeItemID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public Nullable<bool> Active { get; set; }

        public ProductDetailAttributeItemDto AttributeItem { get; set; }
        public ProductDetailAttributeTypeDto AttributeType { get; set; }
    }
}
