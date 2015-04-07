using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Routing;
using System.Web;

namespace Common.Web.Mvc.Castle
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private IWindsorContainer container;

        /// <summary>
        /// Creates a new instance of the <see cref="WindsorControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The Windsor container instance to use when creating controllers.</param>
        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            this.container.Release(controller);
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found or it does not implement IController.", context.HttpContext.Request.Path));
            }

            var controller = this.container.Resolve(controllerType) as Controller;

            //if (controller != null)
            //{
            //    controller.ActionInvoker = container.Resolve<IActionInvoker>();
            //}

            return controller;
        }
    }
}
