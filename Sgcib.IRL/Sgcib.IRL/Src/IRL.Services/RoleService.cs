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
    public class RoleService : IRoleService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly ISecurityRepository<Role> roleRepository;
        
        public RoleService(ISecurityUnitOfWork securityUnitOfWork,
                            ISecurityRepository<Role> roleRepository)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.roleRepository = roleRepository;
        }

        public Role GetRoleByName(string name)
        {
            return roleRepository.Get(x => string.Compare(x.Name, name, true) == 0);
        }

        public IList<Role> GetAllRoles()
        {
            return roleRepository.GetAll().ToList();
        }

        public IList<Role> GetRolesForUser(string userName)
        {
            return roleRepository.GetMany(x => x.UserRole.Any(u => u.User != null && string.Compare(u.User.ADName, userName, true) == 0))
                                 .ToList();
        }

        public Role GetRoleById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("The role id must not be empty");
            }

            return roleRepository.Get(x => x.Id == id);
        }

        public void CreateRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("The role object must not be null");
            }

            if (string.IsNullOrEmpty(role.Name))
            {
                throw new ArgumentNullException("The role name must not be null or empty");
            }

            role.Id = Guid.NewGuid();
            roleRepository.Add(role);
            securityUnitOfWork.Commit();
        }

        public void UpdateRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("The role object must not be null");
            }

            if (role.Id == Guid.Empty)
            {
                throw new ArgumentException("The role id must not be empty");
            }

            roleRepository.Update(role, x => x.Id);
            securityUnitOfWork.Commit();
        }

        public void DeleteRole(Guid guid)
        {
            var role = roleRepository.Get(x => x.Id == guid);

            if (role == null)
            {
                throw new ArgumentNullException("Guid don't represent a valid role");
            }

            roleRepository.Delete(role);
            securityUnitOfWork.Commit();
        }
    }
}
