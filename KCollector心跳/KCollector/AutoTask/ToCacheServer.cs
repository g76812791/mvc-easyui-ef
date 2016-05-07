using KCollector.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace KCollector.AutoTask
{
    /// <summary>
    /// Redis中保存两个缓存空间
    /// 一种是List类型 做队列使用 缓存时间设置十分钟过期时间
    /// 一种是SortedList类型用于统计排名等 设置24小时过期时间
    /// 
    /// </summary>
    public class ToCacheServer
    {

        public static void Run()
        {
            while (true)
            {
                WriteToCacheServer();
                Thread.Sleep(1 * 1 * 10 * 1000);
            }
        }

        private static void WriteToCacheServer()
        {
            List<Models.SearchWord> sWordList = MoveQueue();

            ToCacheList(sWordList);

        }

        private static void ToCacheList(List<SearchWord> sWordList)
        {
            if (sWordList == null)
                return;

            using (IRedisClient redisCli = Common.Common.RedisPool.GetClient())
            {
                //添加到list中十分钟后失效用于及时应用
                var list = sWordList.Select(l => l.key).ToList<string>();
                redisCli.AddRangeToList(Common.ConfigSettings.RedisListKey, list);

                if (redisCli.GetTimeToLive(Common.ConfigSettings.RedisListKey).TotalSeconds < 0)
                    redisCli.ExpireEntryIn(Common.ConfigSettings.RedisListKey, new TimeSpan(0, 10, 0));


                //添加到有序集合中用于统计
                foreach (var item in sWordList)
                {

                    //redisCli.AddItemToSortedSet(RedisListKey, item.key, Convert.ToInt64(item.dt.ToString("yyyyMMddHHmmss")));                    
                    redisCli.IncrementItemInSortedSet(Common.ConfigSettings.RedisSortListKey, item.key, 1);
                }

                if (redisCli.GetTimeToLive(Common.ConfigSettings.RedisSortListKey).TotalSeconds < 0)
                    redisCli.ExpireEntryIn(Common.ConfigSettings.RedisSortListKey, new TimeSpan(24, 0, 0));


                // redisCli.GetRangeWithScoresFromSortedSet(RedisListKey,;

            }
        }


        private static List<SearchWord> MoveQueue()
        {
            if (Common.Common.QueueKeyWord.IsEmpty)
            {
                return null;
            }
            List<SearchWord> sWordList = new List<SearchWord>();


            for (int i = 0; i < Common.Common.QueueKeyWord.Count; i++)
            {
                SearchWord sWord = new SearchWord();
                Common.Common.QueueKeyWord.TryDequeue(out sWord);
                if (sWord != null)
                {
                    sWordList.Add(sWord);
                }
            }


            return sWordList;
        }

    }
}