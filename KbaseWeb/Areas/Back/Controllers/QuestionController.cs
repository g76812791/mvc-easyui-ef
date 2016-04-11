using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Comm;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class QuestionController :BaseController<question>
    {
        public QuestionController()
        {
            base.fileds = new[] {"Title", "Content"};
        }
        //
        // GET: /Back/Question/

        public ActionResult Index()
        {
            return View();
        }

        public override ActionResult GetList(question en)
        {
            listWhere = ExpressExtens.True<question>();
            if (!string.IsNullOrEmpty(en.Title))
            {
                listWhere = listWhere.And((q) => q.Title.Contains(en.Title));
            }
            listOrder = "Id";
            return base.GetList(en);
        }

    }
}
