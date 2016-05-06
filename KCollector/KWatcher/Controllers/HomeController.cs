
using KCollector.Common;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWatcher.Controllers
{
    public class HomeController : Controller
    {
        public readonly static string RedisSortListKey = "KCollector_SearchWord_SortList";
        public readonly static string RedisListKey = "KCollector_SearchWord_List";//KCollector_SearchWord_List
        public ActionResult Index()
        {
            int count = 0;
            using (IRedisClient redisCli = Common.RedisPool.GetClient())
            {
                count = redisCli.GetSortedSetCount(RedisSortListKey);//获取总数
            }
            ViewBag.count = count;
            return View();
        }
        public ActionResult Danmu()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}