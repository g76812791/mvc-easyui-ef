using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class RoleController : BaseController<role>
    {
        public RoleController()
        {
            fileds = new[] {"Name","Detail"};
        }

        public ActionResult Index()
        {
            return View();
        }

        public override ActionResult GetList(role en)
        {
            listWhere = ExpressExtens.True<role>();

            if (!string.IsNullOrEmpty(en.Name))
            {
                listWhere = listWhere.And(q => q.Name.Contains(en.Name.Trim()));
            }
            listOrder = "CreateTime";
            return base.GetList(en);
            
        }
    }
}
