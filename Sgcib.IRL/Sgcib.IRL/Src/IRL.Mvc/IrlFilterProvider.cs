using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;

namespace IRL.Mvc
{
    public class IrlFilterProvider : IFilterProvider
    {
        private IWindsorContainer container;

        public IrlFilterProvider(IWindsorContainer container)
        {
            this.container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var authorizationFilters = container.ResolveAll<IAuthorizationFilter>();

            foreach (var authorizationFilter in authorizationFilters)
            {
                yield return new Filter(authorizationFilter, FilterScope.Global, null);
            }
        }
    }
}
