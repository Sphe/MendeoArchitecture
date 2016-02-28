using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using System.Web;

namespace Common.Web.Mvc.Castle
{
    public class WebWindsorRoleProvider : WindsorRoleProvider
    {
        public override IWindsorContainer GetContainer()
        {
            var context = HttpContext.Current;
            if (context == null)
                throw new Exception("No HttpContext");
            var accessor = context.ApplicationInstance as IContainerAccessor;
            if (accessor == null)
                throw new Exception("The global HttpApplication instance needs to implement " + typeof(IContainerAccessor).FullName);
            if (accessor.Container == null)
                throw new Exception("HttpApplication has no container initialized");
            return accessor.Container;
        }
    }
}
