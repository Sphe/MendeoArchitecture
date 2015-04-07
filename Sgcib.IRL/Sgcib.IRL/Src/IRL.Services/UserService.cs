using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.Security;
using IRL.Data.Security.Infrastructure;
using IRL.Services.Contracts;

namespace IRL.Services
{
    public class UserService : IUserService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly ISecurityRepository<User> userRepository;

        public UserService(ISecurityUnitOfWork securityUnitOfWork,
                            ISecurityRepository<User> userRepository)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.userRepository = userRepository;
        }

        public IList<User> GetAllUsers()
        {
            return userRepository.GetAll().ToList();
        }

        public User GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id must not be empty");
            }

            return userRepository.Get(x => x.Id == id);
        }

        public User GetUserByName(string name)
        {
            return userRepository.Get(x => string.Compare(x.ADName, name, true) == 0);
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user cannot be null");
            }

            if (string.IsNullOrEmpty(user.ADName))
            {
                throw new ArgumentNullException("user must have a valid ADName");
            }

            user.Id = Guid.NewGuid();

            userRepository.Add(user);
            securityUnitOfWork.Commit();
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user cannot be null");
            }

            if (user.Id == Guid.Empty)
            {
                throw new ArgumentNullException("user id must not be empty");
            }

            userRepository.Update(user, x => x.Id);
            securityUnitOfWork.Commit();
        }

        public void DeleteUser(Guid guid)
        {
            var user = userRepository.Get(x => x.Id == guid);

            if (user == null)
            {
                throw new ArgumentNullException("Guid don't represent a valid user");
            }

            userRepository.Delete(user);
            securityUnitOfWork.Commit();
        }
    }
}
