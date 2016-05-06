using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KCollector.Common
{
    public class ConfigSettings
    {
        /// <summary>
        /// Redis服务器IP
        /// </summary>
        public static readonly string RedisServerIP = ConfigurationManager.AppSettings["redisServerIP"].ToString();

        /// <summary>
        /// Redis服务器端口
        /// </summary>
        public static readonly string RedisServerPort = ConfigurationManager.AppSettings["redisServerPort"].ToString();

        /// <summary>
        /// Redis服务器端口
        /// </summary>
        public static readonly string RedisSortListKey = ConfigurationManager.AppSettings["RedisSortListKey"].ToString();
        /// <summary>
        /// Redis服务器端口
        /// </summary>
        public static readonly string RedisListKey = ConfigurationManager.AppSettings["RedisListKey"].ToString();



    }
}