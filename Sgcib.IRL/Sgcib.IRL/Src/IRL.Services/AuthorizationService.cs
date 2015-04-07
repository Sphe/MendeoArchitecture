using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Services.Contracts;
using IRL.Model.Security;
using IRL.Data.Security.Infrastructure;
using System.Diagnostics;
using System.Threading;

namespace IRL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISecurityRepository<WebSiteAccessPermission> webSiteAccessPermissionRepository;

        public AuthorizationService(ISecurityRepository<WebSiteAccessPermission> webSiteAccessPermissionRepository)
        {
            this.webSiteAccessPermissionRepository = webSiteAccessPermissionRepository;
        }

        public bool IsAuthorizeForAction(string role, string controller, string action)
        {
            return IsAuthorizeForAction(new string[] { role }, controller, action);
        }

        public bool IsAuthorizeForAction(string[] roles, string controller, string action)
        {
            var permissionsOnControllerAction = webSiteAccessPermissionRepository.GetMany(x => 
                                                        string.Compare(x.Controller, controller, true) == 0
                                                        && string.Compare(x.Action, action, true) == 0
                                                        && roles.Contains(x.Role.Name)
                                                        && x.Enabled == true);

            if (permissionsOnControllerAction.Count() > 0)
            {
                return true;
            }

            var permissionsOnController = webSiteAccessPermissionRepository.GetMany(x =>
                                                string.Compare(x.Controller, controller, true) == 0
                                                && string.IsNullOrEmpty(x.Action)
                                                && roles.Contains(x.Role.Name)
                                                && x.Enabled == true);

            if (permissionsOnController.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
