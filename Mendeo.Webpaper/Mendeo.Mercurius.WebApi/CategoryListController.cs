using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Mvc;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Mercurius.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http;

namespace Mendeo.Mercurius.WebApi
{
    public class CategoryListController : ApiController
    {
        private readonly IRequestingProductService _requestingProductService;
        private ApplicationUserManager _userManager;

        public CategoryListController(IRequestingProductService requestingProductService)
        {
            _requestingProductService = requestingProductService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public RequestIndexedProductResponse GetProduct([FromBody]JToken jsonbody)
        {
            var request = jsonbody.ToObject<RequestIndexedProduct>();

            request.Size = request.Size == 0 ? Int32.MaxValue : request.Size;

            return _requestingProductService.RequestProductPaginated(request);
        }

        [HttpPost]
        public async Task<RequestIndexedProductResponse> GetProductByUser([FromBody]JToken jsonbody)
        {
            var request = jsonbody.ToObject<RequestIndexedProduct>();
            request.UserId = null;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return new RequestIndexedProductResponse()
                {
                    StatusCode = 500
                };
            }

            request.Size = request.Size == 0 ? Int32.MaxValue : request.Size;
            request.UserId = user.UserId;

            return _requestingProductService.RequestProductPaginated(request);
        }
    }
}
