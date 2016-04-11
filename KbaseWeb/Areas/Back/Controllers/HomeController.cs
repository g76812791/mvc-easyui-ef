using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class HomeController : BaseController<user>
    {
        //
        // GET: /Back/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
