using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Comm
{
    public static  class ClientIp
    {
        /// <summary>
        /// 获取客户端IP-重载
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            string clientIP = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(clientIP))
            {
                clientIP = string.Empty;
            }
            if (String.IsNullOrEmpty(clientIP))
            {
                clientIP = request.ServerVariables["REMOTE_ADDR"];
            }

            if (!String.IsNullOrEmpty(clientIP)
                && clientIP.Contains(","))
            {
                string[] tempIp = clientIP.Split(',');
                clientIP = tempIp[tempIp.Length - 1].Trim();
            }
            try
            {
                System.Net.IPAddress.Parse(clientIP);
            }
            catch
            {
                clientIP = "127.127.0.1";
            }

            return clientIP;
        }
    }
}
