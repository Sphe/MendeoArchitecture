using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Mvc;
using Mendeo.Mercurius.Service.Contract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Mendeo.Mercurius.WebApi.PeopleOnline
{
    public class PeopleOnlineApiFilter : ActionFilterAttribute, IApiMendeoFilter
    {
        private readonly IPeopleOnlineService _peopleOnlineService;

        public PeopleOnlineApiFilter(IPeopleOnlineService peopleOnlineService)
        {
            _peopleOnlineService = peopleOnlineService;
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var clientInfo = await GetClientInfo(actionExecutedContext.Request, 
                actionExecutedContext.ActionContext != null &&
                actionExecutedContext.ActionContext.RequestContext != null ? 
                actionExecutedContext.ActionContext.RequestContext.Principal : null);

            _peopleOnlineService.EnqueuePeopleOnline(clientInfo.Item1, clientInfo.Item2, clientInfo.Item3);
        }

        private async Task<Tuple<string, string, int?>> GetClientInfo(HttpRequestMessage request, IPrincipal principal)
        {
            int? userId = null;
            
            if (principal != null && principal.Identity != null)
            {
                var um = request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await um.FindByIdAsync(principal.Identity.GetUserId());
                if (user != null)
                {
                    userId = user.UserId;
                }
            }

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var requestContext = ((HttpContextWrapper) request.Properties["MS_HttpContext"]);

                return new Tuple<string, string, int?>(requestContext.Request.UserHostAddress,
                    requestContext.Request.UserAgent,
                    userId);
            }

            return HttpContext.Current != null ? 
                new Tuple<string, string, int?>(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent, userId) : null;
        }
    }
}
