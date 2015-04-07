using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using IRL.Services.Contracts;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace IRL.Mvc.Security
{
    public class IrlAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IRoleService roleService;

        public IrlAuthorizationFilter(IAuthorizationService authorizationService, IRoleService roleService)
        {
            this.authorizationService = authorizationService;
            this.roleService = roleService;
        }

        protected virtual bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            var currentUserIdentity = httpContext.User.Identity;

            if (!currentUserIdentity.IsAuthenticated)
            {
                return false;
            }

            // Error Controller, no permissions check.
            if (controller.StartsWith("Error", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var currentRoles = roleService.GetRolesForUser(currentUserIdentity.Name)
                                                .Select(x => x.Name)
                                                .ToArray();

            return authorizationService.IsAuthorizeForAction(currentRoles, controller, action);
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() {  
                { "controller", "Error" },
                { "action", "AccessDenied" },
                { "urlReached", filterContext.HttpContext.Request.RawUrl }
            });
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
    }
}
