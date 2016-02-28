using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mendeo.Mercurius.WebApi
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController()
        {
            //_categoryService = categoryService;
        }

        [HttpGet]
        public CategoryComponent GetCategoryTree()
        {
            return null;
        }

        [HttpGet]
        public IList<CategoryComponent> GetBreadCrumb(int categoryId)
        {
            return _categoryService.GetBreadCrumbFromCategoryId(categoryId);
        }
    }
}
