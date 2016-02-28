using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto.ProductDetail;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain.Enum;

namespace Mendeo.Mercurius.Dto.ProductFavorite
{
    public class ProductFavoriteProductDetailDto : BaseDto
    {
        public ProductDetailProductDto ProductDetailProductDto { get; set; }

        public ProductFavoriteDto ProductFavoriteDto { get; set; }
    }
}
