using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRL.Model.Security;

namespace IRL.Services.Contracts
{
    public interface IUserService
    {
        IList<User> GetAllUsers();

        User GetUserById(Guid id);

        User GetUserByName(string name);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid guid);
    }
}
