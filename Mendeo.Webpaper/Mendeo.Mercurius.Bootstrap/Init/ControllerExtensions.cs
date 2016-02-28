using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mendeo.Mercurius.Bootstrap.Init
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// The ExpressionHelper.GetRouteValuesFromExpression() method in MVC Futures will 
        /// put all parameters from the lambda expression into the route value dictionary,
        /// but if the parameter is a reference type, that doesn't make sense and leads to 
        /// URLs like http://mysite.com/account/add?model=MyProject.AccountViewModel and
        /// extraneous errors in ModelState.  So we'll strip out those reference types
        /// in here.
        /// 
        /// If you really wanted to have a reference type in the route value dictionary,
        /// you should override ToString() in the object and have it return something 
        /// meaningful that could be added to the route value dictionary.  If you do that,
        /// this method will see the route value as a string and will not strip it out.
        /// </summary>
        /// <param name="dictionary"></param>
        private static void RemoveReferenceTypesFromRouteValues(RouteValueDictionary dictionary)
        {
            var keysToRemove = new List<string>();
            foreach (var pair in dictionary)
            {
                if (pair.Value != null && !(pair.Value is string || pair.Value.GetType().IsSubclassOf(typeof(ValueType))))
                {
                    keysToRemove.Add(pair.Key);
                }
            }

            foreach (string key in keysToRemove)
            {
                dictionary.Remove(key);
            }
        }

        /// <summary>
        /// Determines whether the specified type is a controller
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if type is a controller, otherwise false</returns>
        public static bool IsController(Type type)
        {
            return type != null
                //                               && type.IsPublic
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof(IController).IsAssignableFrom(type);
        }

        public static bool IsApiController(Type type)
        {
            return type != null
                //                               && type.IsPublic
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof(ApiController).IsAssignableFrom(type);
        }
    }
}
