using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using KbaseData;
using KbaseWeb.Models;
using Entity;

namespace KbaseWeb.Controllers
{
    public class KbaseController : Controller
    {
        //
        // GET: /Kbase/
        UserDal dal = new UserDal();
        BaseDal<anlidetails> ddal = new BaseDal<anlidetails>();
        BaseDal<anlitype> tdal = new BaseDal<anlitype>();
        BaseDal<question> qdal = new BaseDal<question>();
        public ActionResult Index()
        {
           
            ViewBag.Flag = 1;
            return View();
        }


        public ActionResult Product()
        {
            ViewBag.Flag = 2;
            return View();
        }
        public ActionResult Cases()
        {
           // var tdal = new BaseDal<anlitype>();
            //var ddal = new BaseDal<anlidetails>();
            List<caseview> cases = new List<caseview>();
            var types = tdal.GetListTopN(p => 1 == 1, "OrderNum", false, 0).ToList();
            List<long> ids = types.Select(p => p.Id).ToList();
            var detalis = ddal.GetListTopN(p => ids.Contains((long)p.AId), "Id", true, 0).ToList();
            foreach (var d in types)
            {
                caseview casemodel = new caseview();
                casemodel.type = d;
                casemodel.detalis = detalis.Where(p => p.AId == d.Id).ToList();
                cases.Add(casemodel);
            }
            //var detalis = ddal.GetListTopN(p => 1 == 1, "Id", true, 0).ToList();
            //var details = 
            ViewBag.Flag = 3;
            return View(cases);
        }

        public ActionResult ShowCases(int Id)
        {
            var data = ddal.GetOne(p => p.Id == Id);
            return View(data);
        }
        public ActionResult Download()
        {
            ViewBag.Flag = 4;
            return View();
        }
        public ActionResult Question()
        {
            var qdata = qdal.GetListTopN(p=>1==1,"CreatTime",true,0).ToList();
            ViewBag.Flag = 5;
            return View(qdata);
        }
    }
}
