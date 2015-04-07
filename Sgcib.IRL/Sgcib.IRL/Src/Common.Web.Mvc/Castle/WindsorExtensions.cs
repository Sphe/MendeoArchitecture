using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Web.Mvc;

using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;

namespace Common.Web.Mvc.Castle
{
    public static class WindsorExtensions
    {
        /// <summary>
        /// Searches for the first interface found associated with the
        /// <see cref = "ServiceDescriptor" /> which is not generic and which
        /// is found in the specified namespace.
        /// </summary>
        public static BasedOnDescriptor FirstNonGenericCoreInterface(
            this ServiceDescriptor descriptor, string interfaceNamespace)
        {
            return descriptor.Select(
                delegate(Type type, Type[] baseType)
                    {
                        var interfaces =
                            type.GetInterfaces().Where(
                                t => t.IsGenericType == false && t.Namespace.StartsWith(interfaceNamespace));

                        if (interfaces.Count() > 0)
                        {
                            return new[] { interfaces.ElementAt(0) };
                        }

                        return null;
                    });
        }

        public static IWindsorContainer RegisterController<T>(this IWindsorContainer container) where T : IController
        {
            container.RegisterControllers(typeof(T));
            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Type[] controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                if (ControllerExtensions.IsController(type))
                {
                    container.Register(Component.For(type).Named(type.FullName.ToLower()).LifeStyle.Is(LifestyleType.Transient));
                }
            }

            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                container.RegisterControllers(assembly.GetExportedTypes());
            }
            return container;
        }

        public static void InjectProperties(this IKernel kernel, object target)
        {
            var type = target.GetType();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanWrite && kernel.HasComponent(property.PropertyType))
                {
                    var value = kernel.Resolve(property.PropertyType);

                    try
                    {
                        property.SetValue(target, value, null);
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Error setting property {0} on type {1}, See inner exception for more information.", property.Name, type.FullName);
                        throw new ComponentActivatorException(message, ex, null);
                    }
                }
            }
        }
    }
}
