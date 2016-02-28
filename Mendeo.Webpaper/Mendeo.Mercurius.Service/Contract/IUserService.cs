using Mendeo.Mercurius.Model.FullDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IUserService
    {
        EntityWithValidation<User> CreateUser(string login, string email, string phone);
        User RetrieveUserById(int userID);
    }
}
