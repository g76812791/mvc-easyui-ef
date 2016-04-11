using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace KbaseWeb.Filters
{
    public class ImageFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //string miwen = AESHelper.AESEncrypt("123456", "klsaflkasdjasgioajsgljsdlkjaslkjfslkdfjasl");
            //string mingwen = AESHelper.AESDecrypt(miwen, "klsaflkasdjasgioajsgljsdlkjaslkjfslkdfjasl");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //HttpResponseMessage result = new HttpResponseMessage { Content = new StreamContent(actionExecutedContext.Response.Content, "application/json") };
            //actionExecutedContext.Response.Headers = new System.Net.Http.Headers.HttpResponseHeaders();
            //actionExecutedContext.Response.Headers.Add("Content-type", "image/jpeg");
            actionExecutedContext.Response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            //var str = actionExecutedContext.Response.Content.ToString();
        }
    }
}