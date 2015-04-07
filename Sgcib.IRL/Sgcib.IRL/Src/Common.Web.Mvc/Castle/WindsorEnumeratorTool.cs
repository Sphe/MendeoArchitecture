using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Web.Mvc.Castle.Contract;
using Castle.MicroKernel;
using System.Web.Mvc;
using System.Reflection;

namespace Common.Web.Mvc.Castle
{
    public class WindsorEnumeratorTool : IWindsorEnumeratorTool
    {
        private readonly IKernel kernel;

        public WindsorEnumeratorTool(IKernel kernel)
        {
            this.kernel = kernel;
        }

        private IList<Type> RetrieveAllTypeFromWindsor(Type type)
        {
            return kernel.GetAssignableHandlers(type)
                            .Select(x => x.ComponentModel.Implementation)
                            .ToList();
        }

        public IList<string> GetAllControllerNames()
        {
            return RetrieveAllTypeFromWindsor(typeof(IController))
                            .Select(x => x.Name.Replace("Controller", string.Empty))
                            .OrderBy(x => x)
                            .ToList();
                            
        }

        public IEnumerable<string> GetAllActionNames(string controller)
        {
            var controllerTypeString = string.Concat(controller, "Controller");

            var controllerType = RetrieveAllTypeFromWindsor(typeof(IController))
                                    .Where(x => x.Name.Contains(controller))
                                    .FirstOrDefault();

            if (controllerType != null)
            {
                var methodInfos = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.ReturnType != typeof(ActionResult))
                    {
                        continue;
                    }

                    yield return methodInfo.Name;
                }
            }
        }
    }
}
