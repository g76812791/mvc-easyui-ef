using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comm;
using KbaseWeb.Areas.Back.Controllers;
using KbaseWeb.Common;

namespace KbaseWeb.Filters
{
    /// <summary>
    /// 验证特性（验证是否拥登录）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class IsLoginAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="filterContext">上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                object[] actionFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoCompress), false);
                object[] controllerFilter = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(NoCompress), false);
                if (controllerFilter.Length == 1 || actionFilter.Length == 1)
                {
                    return;
                }
                bool flag = AES.UrlEncrypt(CookieHelp.GetCookieValByKey("Userid") + CookieHelp.GetCookieValByKey("LoginName")) == AES.UrlEncrypt(CookieHelp.GetCookieValByKey("long"));
                if (string.IsNullOrEmpty(CookieHelp.GetCookieValByKey("Userid")) && !flag)
                {
                    HttpContext.Current.Response.Clear();
                    filterContext.Result = new RedirectResult(AppConfig.LoginUrl);
                    HttpContext.Current.Response.Write("<script>parent.parent.window.location='" + AppConfig.LoginUrl + "';parent.window.location='" + AppConfig.LoginUrl + "'</script>");
                    HttpContext.Current.Response.End();
                    filterContext.Result = new EmptyResult();
                }
                base.OnActionExecuting(filterContext);
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(exception.Message);
                HttpContext.Current.Response.End();
                filterContext.Result = new EmptyResult();
            }
        }
    }

    /// <summary>
    /// 验证特性（验证是否拥有控制器操作权限）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CheckAc : ActionFilterAttribute, IActionFilter
    {
        public string OpName = string.Empty;
        public string UserId = string.Empty;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string a = filterContext.ActionDescriptor.ActionName;
            //string b = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //var temp = filterContext.ActionParameters;
            var controllerName = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
            var actionName = (filterContext.RouteData.Values["action"]).ToString().ToLower();
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString();
            if (!Permission.isHasQuanXian(areaName + "." + controllerName + "."+actionName))
            {
                HttpContext.Current.Response.Write("<script>alert('没有相关权限')</script>");
                HttpContext.Current.Response.End();
                filterContext.Result = new EmptyResult();
           
            }
            
        }

    }

}