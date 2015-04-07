using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRL.Services.Contracts
{
    public interface IAuthorizationService
    {
        bool IsAuthorizeForAction(string role, string controller, string action);

        bool IsAuthorizeForAction(string[] roles, string controller, string action);
    }
}
