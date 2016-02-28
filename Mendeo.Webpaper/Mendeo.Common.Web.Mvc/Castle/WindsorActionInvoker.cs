using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.MicroKernel;

namespace Common.Web.Mvc.Castle
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IKernel kernel;

        public WindsorActionInvoker(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);

            foreach (var actionFilter in filters.ActionFilters)
            {
                kernel.InjectProperties(actionFilter);
            }

            foreach (var authorizationFilter in filters.AuthorizationFilters)
            {
                kernel.InjectProperties(authorizationFilter);
            }

            foreach (var exceptionFilter in filters.ExceptionFilters)
            {
                kernel.InjectProperties(exceptionFilter);
            }

            foreach (var resultFilter in filters.ResultFilters)
            {
                kernel.InjectProperties(resultFilter);
            }

            return filters;
        }
    }
}
