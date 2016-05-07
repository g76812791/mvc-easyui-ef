using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //test addqueue()
            //Common.Common.AddQueue("test");

            return View();
        }

        public ActionResult vrlist()
        {
            List<string> list = null;
            List<string> sortList = null;
            long count = 0;
            long sortcount = 0;

            using (ServiceStack.Redis.IRedisClient cl = Common.Common.RedisPool.GetClient())
            {
                count = cl.GetListCount(Common.ConfigSettings.RedisListKey);

                list = cl.GetRangeFromList(Common.ConfigSettings.RedisListKey, count > 100 ? (int)(count - 100) : 0, (int)count - 1);

                sortList = cl.GetRangeFromSortedSetDesc(Common.ConfigSettings.RedisSortListKey, 1, 10);

                sortcount = cl.GetSortedSetCount(Common.ConfigSettings.RedisSortListKey);
            }
            ViewBag.list = string.Join(",",list.ToArray());
            ViewBag.count = count.ToString();
            ViewBag.qcount = Common.Common.GetQueueCount().ToString();
            ViewBag.sortlist = string.Join(",", sortList.ToArray());
            ViewBag.sortcount = sortcount;

            int maxthreadcount = 0;
            //int currthreadcount = 0;
            int availthreadcount = 0;
            int iocouint = 0;

            ThreadPool.GetMaxThreads(out maxthreadcount, out iocouint);
            ThreadPool.GetAvailableThreads(out availthreadcount, out iocouint);

            ViewBag.maxthreadcount = maxthreadcount;

            ViewBag.availthreadcount = availthreadcount;



            return View();
        }
       

    }
}
