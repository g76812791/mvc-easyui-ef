using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using KWatcher.Models;

namespace KCollector.Common
{
    public class Common
    {
        /// <summary>
        /// Redis连接池
        /// </summary>
        public static PooledRedisClientManager RedisPool = new PooledRedisClientManager(4000, 20, ConfigSettings.RedisServerIP + ":" +
           ConfigSettings.RedisServerPort);

        public static ConcurrentQueue<SearchWord> QueueKeyWord = new ConcurrentQueue<SearchWord>();

        public static void AddQueue(string searchWord)
        {
            try
            {
                QueueKeyWord.Enqueue(new SearchWord() { key = searchWord, dt = DateTime.Now });

            }
            catch 
            {
                new ErrorLog().Log("添加队列错误");
            }
        }
    }
}