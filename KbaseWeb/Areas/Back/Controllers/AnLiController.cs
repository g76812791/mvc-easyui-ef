using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class AnLiController : BaseController<anlidetails>
    {
        public AnLiController()
        {
            base.fileds = new[] {"AId", "Title", "Content"};
        }
        //
        // GET: /Back/AnLi/

        public ActionResult Index()
        {
            return View();
        }




        [ValidateInput(false)]
        public override ActionResult Add(anlidetails en )
        {
            return base.Add(en);
        }
        [ValidateInput(false)]
        public override ActionResult Update(anlidetails en)
        {
            return base.Update(en);
        }
        
    }
}
