using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class ProductController : BaseController<product>
    {
        //
        // GET: /Back/Product/
        public ProductController()
        {

            base.fileds = new[] { "Title", "Content", "OrderNum", "Anchor" };
        }


        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public override ActionResult Add(product en)
        {
            return base.Add(en);
        }

        [ValidateInput(false)]
        public override ActionResult Update(product en)
        {
            return base.Update(en);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="en">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(product en)
        {
            listWhere = ExpressExtens.True<product>();
            if (!string.IsNullOrEmpty(en.Title))
            {
                listWhere = listWhere.And(q => q.Title.Contains(en.Title.Trim()));
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
