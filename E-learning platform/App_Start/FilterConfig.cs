using E_learning_platform.Filters;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalActionFilter());
        }
    }
}
