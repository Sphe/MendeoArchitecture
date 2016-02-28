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
    public class ProductFavoriteDto : BaseDto
    {
        public int ProductFavoriteID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public System.DateTime CreatedOn { get; set; }

        public bool ToRemove { get; set; }
    }
}
