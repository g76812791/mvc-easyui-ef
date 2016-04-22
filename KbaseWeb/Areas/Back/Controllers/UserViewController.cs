using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class UserViewController : BaseController<view_user>
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="p">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(view_user p)
        {
            listWhere = ExpressExtens.True<view_user>();
            if (!string.IsNullOrEmpty(p.LoginName))
            {
                listWhere = listWhere.And((q) => q.LoginName.Contains(p.LoginName));
            }
            listOrder = "Id";
            return base.GetList(p);
        }

    }
}
