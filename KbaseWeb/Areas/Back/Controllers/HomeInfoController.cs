using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Comm;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class HomeInfoController : BaseController<homeinfo>
    {
        public HomeInfoController()
        {
            fileds = new[] {"Title","Content","OrderNum"};
        }
        public ActionResult Index()
        {
            return View();
        }
        public override ActionResult GetList(homeinfo en)
        {
            listWhere = ExpressExtens.True<homeinfo>();
            if (!string.IsNullOrEmpty(en.Title))
            {
                listWhere = listWhere.And(q => q.Title.Contains(en.Title.Trim()));
            }
            listOrder = "OrderNum";
            return base.GetList(en);
        }
        [ValidateInput(false)]
        public override ActionResult Add(homeinfo en)
        {
            return base.Add(en);
        }
        [ValidateInput(false)]
        public override ActionResult Update(homeinfo en)
        {
            return base.Update(en);
        }
    }
}
