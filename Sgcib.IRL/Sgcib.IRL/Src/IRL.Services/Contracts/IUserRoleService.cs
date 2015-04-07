using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.Security;

namespace IRL.Services.Contracts
{
    public interface IUserRoleService
    {
        IList<UserRole> GetAllUserRoles();

        UserRole GetUserRoleById(int id);

        void CreateUserRole(UserRole userRole);

        void UpdateUserRole(UserRole userRole);

        void DeleteUserRole(int id);
    }
}
