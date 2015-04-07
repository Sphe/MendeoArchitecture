using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Services.Contracts;
using IRL.Model.Security;
using IRL.Data.Security.Infrastructure;

namespace IRL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly ISecurityRepository<UserRole> userRoleRepository;
        
        public UserRoleService(ISecurityUnitOfWork securityUnitOfWork,
                                ISecurityRepository<UserRole> userRoleRepository)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.userRoleRepository = userRoleRepository;
        }

        public IList<UserRole> GetAllUserRoles()
        {
            return userRoleRepository.GetAll().ToList();
        }

        public UserRole GetUserRoleById(int id)
        {
            return userRoleRepository.Get(x => x.Id == id);
        }

        public void CreateUserRole(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException("The user role object must not be null");
            }

            userRoleRepository.Add(userRole);
            securityUnitOfWork.Commit();
        }

        public void UpdateUserRole(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException("The role object must not be null");
            }

            if (userRole.Id <= 0)
            {
                throw new ArgumentException("The user role id must be valid");
            }

            userRoleRepository.Update(userRole, x => x.Id);
            securityUnitOfWork.Commit();
        }

        public void DeleteUserRole(int id)
        {
            var userRole = userRoleRepository.Get(x => x.Id == id);

            if (userRole == null)
            {
                throw new ArgumentNullException("Id don't represent a valid user role");
            }

            userRoleRepository.Delete(userRole);
            securityUnitOfWork.Commit();
        }
    }
}
