using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Castle.Windsor;

namespace Mendeo.Common.WebApi
{
    public class DeoApiFilterProvider : IFilterProvider
    {
        private readonly IWindsorContainer _container;

        public DeoApiFilterProvider(IWindsorContainer container)
        {
            _container = container;
        }


        public IEnumerable<FilterInfo> GetFilters(System.Web.Http.HttpConfiguration configuration, System.Web.Http.Controllers.HttpActionDescriptor actionDescriptor)
        {
            var deoFilters = _container.ResolveAll<IApiMendeoFilter>();

            foreach (var f in deoFilters)
            {
                yield return new FilterInfo(f, FilterScope.Global);
            }
        }
    }
}
