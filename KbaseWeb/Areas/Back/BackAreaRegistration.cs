using System.Web.Mvc;

namespace KbaseWeb.Areas.Back
{
    public class BackAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Back";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Back_default",
                "Back/{controller}/{action}/{id}",
                new { controller = "KBaseLogin", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "KbaseWeb.Areas.Back.Controllers" } //指定该路由查找控制器类的命名空间
            );
        }
    }
}
