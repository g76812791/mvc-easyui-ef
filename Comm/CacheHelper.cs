using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Comm
{
    public class CacheHelper
    {
        
        /// <summary>
        /// T形式设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public static void SetCache(string key, object value, int cacheTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetFromCache<T>(string key, out T value)
        {
            value = default(T);
            object o = HttpRuntime.Cache.Get(key);
            if (o == null) return false;
            value = o is T ? (T)o : default(T);
            return true;
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="logic"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key, Func<T> logic, int cacheTime = 10)
        {

            T value;
            if (GetFromCache(key, out value))
            {
                return value;
            }
            T result = logic();
            SetCache(key, result, cacheTime);
            return result;
        }


        /// <summary>
        /// 字符串形式添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireMinutes"></param>
        public static void AddHttpRuntimeCache(string key, string value, double expireMinutes)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(expireMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 字符串形式获取Http缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetResultFromCache(string key)
        {
            object o = HttpRuntime.Cache.Get(key);
            return o == null ? null : o as string;
        }
    }
}
