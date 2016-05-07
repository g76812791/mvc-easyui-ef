using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using KbaseData;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{

    public class KBaseLoginController : BaseController<user>
    {
         
        // GET: /Back/KBaseLogin/
        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        [NoCompress]
        public ActionResult Index()
        {
            return View();
        }
        [NoCompress]
        public ActionResult Login(string LoginName, string Pwd)
        {
            LoginLogDal logDal = new LoginLogDal();
            user u=GetOne(q => q.LoginName == LoginName.Trim());
            if (u.Pwd == Pwd)
            {
                CookieHelp.SetCookies("UserId", u.Id.ToString(), DateTime.Now.AddDays(1));
                CookieHelp.SetCookies("LoginName", u.LoginName, DateTime.Now.AddDays(1));
                CookieHelp.SetCookies("long", u.Id.ToString() + u.LoginName, DateTime.Now.AddDays(1));
                List<string> permision= new List<string>(){"1","2"};
                CacheHelper.SetCache(base.UserId,permision,30);
                Func<List<string>> cc = () => new List<string>();
                List<string> m = CacheHelper.GetCache(base.UserId, cc);

                logDal.Add(u);
                return Success();
            }
            else
            {
                return Error(new Exception("登陆失败"));
            }
            
        }

        public ActionResult AutoLogin(string Rid)
        {
            return new EmptyResult();
        }

        /// <summary>
        /// 登陆退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit()
        {
//            CookieHelp.ClearCookieValByKey("UserId");
//            CookieHelp.ClearCookieValByKey("LoginName");
            return Success();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public ActionResult UpdatePwd(string Pwd)
        {
            user u = new user();
            u.Id = Convert.ToInt64(CookieHelp.GetCookieValByKey("UserId"));
            u.Pwd = Pwd;
            fileds = new[]  {"Pwd"};
            return base.Update(u);
        }

    }
}
