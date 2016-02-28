using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Dto.ProductDetail
{
    public class ProductDetailImageDto : BaseDto
    {

        public int ImageID { get; set; }
        public Nullable<int> ImageTypeID { get; set; }
        public string ThumbUrl { get; set; }
        public string FullImageUrl { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public bool Active { get; set; }


    }
}
