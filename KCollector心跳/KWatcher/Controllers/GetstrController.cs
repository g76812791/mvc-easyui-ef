using KCollector.Common;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KWatcher.Controllers
{
    public class GetstrController : ApiController
    {
        int mycount = 30;
        public static string RedisSortListKey = ConfigurationManager.AppSettings["RedisSortListKey"].ToString();
        public static string RedisListKey = ConfigurationManager.AppSettings["RedisListKey"].ToString();
        public retobj get(int startcount)
        {
            using (IRedisClient redisCli = Common.RedisPool.GetClient())
            {
                int qcount = 0;//查询数量
                int count = redisCli.GetListCount(RedisListKey);//获取总数
                if (count < startcount)
                    startcount = 0;
                if (count == 0)
                    return new retobj() { success = false, startcount = startcount };
                if(startcount==count)
                    return new retobj() { success = true, startcount = startcount };
                //startcount = count;
                if (startcount == 0)
                    qcount = count > mycount ? mycount : count;
                else
                {
                    qcount = count - startcount;
                    //var mathlog = Math.Log(500, qcount);
                    qcount = qcount > mycount ? mycount : qcount;
                }
                if (qcount == 0)
                    return new retobj() { success = false };
                try
                {
                    List<string> data = redisCli.GetRangeFromList(RedisListKey, count - qcount, count - 1);
                    int speed =  GetSpeed((count - startcount), qcount);
                    return new retobj { speed = speed, startcount = count, strs = data, success = true, count = data.Count };
                }
                catch
                { return new retobj() { success = false }; }
            }
        }
        private int GetSpeed(int tcount, int qcount)
        {
            int speed = 20000;
            int maxmcount = 300;
            int scount = tcount / 10 > qcount / 10 ? tcount / 10 : qcount / 10;
            if (scount > maxmcount)
                scount = 300;
            var mlog = Math.Log(scount, maxmcount);
            int retsped = Convert.ToInt32(35000 - (30000 * mlog));
            return retsped;
        }
    }

    public class retobj
    {
        public int speed { get; set; }
        public int count { get; set; }
        public int startcount { get; set; }
        public List<string> strs { get; set; }
        public bool success { get; set; }
    }
}
