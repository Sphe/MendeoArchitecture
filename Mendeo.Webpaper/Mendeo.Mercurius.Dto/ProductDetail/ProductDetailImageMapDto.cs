using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailImageMapDto : BaseDto
    {
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public int ImageID { get; set; }
        public Nullable<int> ProductImageTypeID { get; set; }
        public bool Activate { get; set; }
        public int Sort { get; set; }
        public string ImageIdentifier { get; set; }

        public ProductDetailImageDto Image { get; set; }
        public ProductDetailImageTypeDto ProductImageType { get; set; }

    }
}
