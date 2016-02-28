using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Mvc;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Mendeo.Common.Core;
using System.Web.Hosting;
using Mendeo.Common.WebApi.FlowJS;
using Mendeo.Mercurius.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.ServiceModel.Channels;
using System.Web;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Dto.ProductFavorite;

namespace Mendeo.Mercurius.WebApi
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly IIndexingProductService _indexingProductService;

        private ApplicationUserManager _userManager;

        public ProductController(IProductService productService, IProductImageService productImageService, IIndexingProductService indexingProductService)
        {
            _productService = productService;
            _productImageService = productImageService;
            _indexingProductService = indexingProductService;
        }
    }
}
