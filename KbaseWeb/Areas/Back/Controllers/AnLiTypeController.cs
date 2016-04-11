using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class AnLiTypeController : BaseController<anlitype>
    {
        //
        // GET: /Back/AnLiType/
        public AnLiTypeController()
        {
            base.fileds = new[] {"Name", "OrderNum"};
        }
        
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
        public override ActionResult GetList(anlitype en)
        {
            listWhere = ExpressExtens.True<anlitype>();
            if (!string.IsNullOrEmpty(en.Name))
            {
                listWhere = listWhere.And (q => q.Name.Contains(en.Name.Trim()));
            }
            listOrder = "OrderNum";
            return base.GetList(en);
        }

        public ActionResult GetTypeToCombo()
        {
            return base.GetListTop("OrderNum");
        }

    }
}
