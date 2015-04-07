using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.Security;

namespace IRL.Services.Contracts
{
    public interface IWebsitePermissionService
    {
        IList<WebSiteAccessPermission> GetAllWebsitePermissions();

        WebSiteAccessPermission GetWebsitePermissionById(int id);

        void CreateWebsitePermission(WebSiteAccessPermission webSiteAccessPermission);

        void UpdateWebsitePermission(WebSiteAccessPermission webSiteAccessPermission);

        void DeleteWebsitePermission(int id);
    }
}
