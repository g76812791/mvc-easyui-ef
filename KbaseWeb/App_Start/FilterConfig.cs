using System.Web;
using System.Web.Mvc;
using KbaseWeb.Filters;

namespace KbaseWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}