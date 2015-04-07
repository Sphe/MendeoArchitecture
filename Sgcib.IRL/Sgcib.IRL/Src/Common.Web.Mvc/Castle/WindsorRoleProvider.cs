using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Castle.Windsor;
using System.Collections.Specialized;
using System.Collections;

namespace Common.Web.Mvc.Castle
{
    public abstract class WindsorRoleProvider : RoleProvider
    {
        private string providerId;

        public abstract IWindsorContainer GetContainer();

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            providerId = config["providerId"];
            if (string.IsNullOrWhiteSpace(providerId))
                throw new Exception(string.Format("Please configure the providerId from the role provider {0}", name));
        }

        private RoleProvider GetProvider()
        {
            try
            {
                var provider = GetContainer().Resolve<object>(providerId) as RoleProvider;
                if (provider == null)
                    throw new Exception(string.Format("Component '{0}' does not inherit RoleProvider", providerId));
                return provider;
            }
            catch (Exception e)
            {
                throw new Exception("Error resolving RoleProvider " + providerId, e);
            }
        }

        private T WithProvider<T>(Func<RoleProvider, T> f)
        {
            var provider = GetProvider();
            try
            {
                return f(provider);
            }
            finally
            {
                GetContainer().Release(provider);
            }
        }

        private void WithProvider(Action<RoleProvider> f)
        {
            var provider = GetProvider();
            try
            {
                f(provider);
            }
            finally
            {
                GetContainer().Release(provider);
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            WithProvider(x => x.AddUsersToRoles(usernames, roleNames));
        }

        public override string ApplicationName
        {
            get
            {
                var provider = GetProvider();
                try
                {
                    return provider.ApplicationName;
                }
                finally
                {
                    GetContainer().Release(provider);
                }
            }
            set
            {
                var provider = GetProvider();
                try
                {
                    provider.ApplicationName = value;
                }
                finally
                {
                    GetContainer().Release(provider);
                }
            }
        }

        public override void CreateRole(string roleName)
        {
            WithProvider(x => x.CreateRole(roleName));
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return WithProvider<bool>(x => x.DeleteRole(roleName, throwOnPopulatedRole));
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return WithProvider<string[]>(x => x.FindUsersInRole(roleName, usernameToMatch));
        }

        public override string[] GetAllRoles()
        {
            return WithProvider<string[]>(x => x.GetAllRoles());
        }

        public override string[] GetRolesForUser(string username)
        {
            return WithProvider<string[]>(x => x.GetRolesForUser(username));
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return WithProvider<string[]>(x => x.GetUsersInRole(roleName));
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return WithProvider<bool>(x => x.IsUserInRole(username, roleName));
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            WithProvider(x => x.RemoveUsersFromRoles(usernames, roleNames));
        }

        public override bool RoleExists(string roleName)
        {
            return WithProvider<bool>(x => x.RoleExists(roleName));
        }
    }
}
