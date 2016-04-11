using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Entity;
using KbaseData;
using KbaseWeb.Areas.Back.Controllers;
using KbaseWeb.Areas.Back.Models;
using Comm;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class UserController : BaseController<user>
    {
        //
        // GET: Back/User/GetUsers
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="p">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(user p)
        {
            listWhere = ExpressExtens.True<user>();
            if (!string.IsNullOrEmpty(p.LoginName))
            {
                listWhere = listWhere.And((q) => q.LoginName.Contains(p.LoginName));
            }
            listOrder = "Id";
            return base.GetList(p);
        }
        public override ActionResult Add(user u)
        {
            Expression<Func<user, bool>> where = q => q.LoginName == u.LoginName.Trim();
            return AddExpress(u, where);
        }

        public override ActionResult Update(user u)
        {
            fileds = new[] { "LoginName", "Pwd" };
            Expression<Func<user, bool>> where = q => q.Id != u.Id;
            where = where.And(q => q.LoginName == u.LoginName.Trim());
            return UpdateExpress(u, fileds, where);
        }
    }
}
