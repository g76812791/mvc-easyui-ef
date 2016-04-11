using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class DaoHangController :BaseController<daohang>
    {
        public DaoHangController()
        {

            base.fileds = new[] {"Name", "Url", "OrderNum","IsOut"};
        }

        //
        // GET: /Back/DaoHang/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="en">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(daohang en)
        {
            listWhere = ExpressExtens.True<daohang>();
            if (!string.IsNullOrEmpty(en.Name))
            {
                listWhere = listWhere.And(q => q.Name.Contains(en.Name.Trim()));
            }
            listOrder = "OrderNum";
            return base.GetList(en);
        }

    }
}
