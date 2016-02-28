using Mendeo.Common.Web.Mvc.Kendo;
using Mendeo.Mercurius.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto.Admin;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Dto.ProductFavorite;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IProductService
    {
        KendoGrid<ProductListSummaryDto> ListAllProductPaginated(KendoGridRequest request);

        Task<ProductDetailProductDto> GetProductDetailById(int productId);

        IList<Tuple<string, string>> ChangeProductStatus(int productId, ProductStatusTypeEnum productStatusType);
    }
}
