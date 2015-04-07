using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.Security;

namespace IRL.Services.Contracts
{
    public interface IRoleService
    {
        IList<Role> GetAllRoles();

        IList<Role> GetRolesForUser(string userName);

        Role GetRoleById(Guid id);

        Role GetRoleByName(string name);        

        void CreateRole(Role role);

        void UpdateRole(Role role);

        void DeleteRole(Guid guid);
    }
}
