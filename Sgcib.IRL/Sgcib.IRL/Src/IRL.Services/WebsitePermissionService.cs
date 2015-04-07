using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Services.Contracts;
using IRL.Data.Security.Infrastructure;
using IRL.Model.Security;

namespace IRL.Services
{
    public class WebsitePermissionService : IWebsitePermissionService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly ISecurityRepository<WebSiteAccessPermission> webSiteAccessPermissionRepository;

        public WebsitePermissionService(ISecurityUnitOfWork securityUnitOfWork,
                                ISecurityRepository<WebSiteAccessPermission> webSiteAccessPermissionRepository)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.webSiteAccessPermissionRepository = webSiteAccessPermissionRepository;
        }

        public IList<WebSiteAccessPermission> GetAllWebsitePermissions()
        {
            return webSiteAccessPermissionRepository.GetAll().ToList();
        }

        public WebSiteAccessPermission GetWebsitePermissionById(int id)
        {
            return webSiteAccessPermissionRepository.Get(x => x.Id == id);
        }

        public void CreateWebsitePermission(WebSiteAccessPermission webSiteAccessPermission)
        {
            if (webSiteAccessPermission == null)
            {
                throw new ArgumentNullException("The web site access permission object must not be null");
            }

            webSiteAccessPermissionRepository.Add(webSiteAccessPermission);
            securityUnitOfWork.Commit();
        }

        public void UpdateWebsitePermission(WebSiteAccessPermission webSiteAccessPermission)
        {
            if (webSiteAccessPermission == null)
            {
                throw new ArgumentNullException("The web site access permission must not be null");
            }

            if (webSiteAccessPermission.Id <= 0)
            {
                throw new ArgumentException("The web site access permission id must be valid");
            }

            webSiteAccessPermissionRepository.Update(webSiteAccessPermission, x => x.Id);
            securityUnitOfWork.Commit();
        }

        public void DeleteWebsitePermission(int id)
        {
            var webSiteAccessPermission = webSiteAccessPermissionRepository.Get(x => x.Id == id);

            if (webSiteAccessPermission == null)
            {
                throw new ArgumentNullException("Id don't represent a valid website access permission");
            }

            webSiteAccessPermissionRepository.Delete(webSiteAccessPermission);
            securityUnitOfWork.Commit();
        }
    }
}
