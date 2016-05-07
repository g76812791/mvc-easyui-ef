using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace KCollector.AutoTask
{
    /// <summary>
    /// 定时删除用于统计的缓存 sortedlist
    /// </summary>
    public class DelCache
    {
        public static void Run()
        {
            while (true)
            {
                DelRedisCache();
                
                //每24小时删除缓存
                Thread.Sleep(24 * 60 * 60 * 1000);
            }
        }

        private static void DelRedisCache()
        {
            throw new NotImplementedException();
        }
    }
}