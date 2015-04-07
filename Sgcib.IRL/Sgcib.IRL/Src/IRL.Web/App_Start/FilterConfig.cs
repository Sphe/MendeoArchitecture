using System.Web;
using System.Web.Mvc;
using IRL.Mvc.Security;

namespace IRL.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}