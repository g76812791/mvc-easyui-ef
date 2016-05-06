using KCollector.Common;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;


namespace KWatcher.Controllers
{

    public class WebSocketController : ApiController
    {
        public static string RedisSortListKey = ConfigurationManager.AppSettings["RedisSortListKey"].ToString();
        public static string RedisListKey = ConfigurationManager.AppSettings["RedisListKey"].ToString();
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        int mycount = 2000;
        private async Task ProcessWSChat(AspNetWebSocketContext arg)
        {
            WebSocket socket = arg.WebSocket;
            int startcount = 0;
            using (IRedisClient redisCli = Common.RedisPool.GetClient())
            {

                while (true)
                {

                    int qcount = 0;//查询数量
                    int count = redisCli.GetListCount(RedisListKey);//获取总数
                    //如果redis数据刚过期
                    if (count < startcount)
                        startcount = 0;
                    if (count == 0)
                        continue;
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
                        continue;
                    try
                    {
                        List<string> data = redisCli.GetRangeFromList(RedisListKey, count - qcount, count - 1);
                        //List<string> data1 = redisCli.GetRangeFromSortedList(RedisListKey, count - qcount, count - 1);
                        //List<string> data = redisCli.GetRangeFromList(RedisListKey, count - 1, count - qcount);
                        if (socket.State == WebSocketState.Open)
                        {
                            ArraySegment<byte> buffer = new ArraySegment<byte>();
                            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"count\":" + GetSpeed((count - startcount), qcount) + ",\"mes\":\"ok\"}"));
                            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else
                        {
                            break;
                        }
                        foreach (var d in data)
                        {

                            Thread.Sleep(10000 / qcount);
                            ArraySegment<byte> buffer = new ArraySegment<byte>();
                            //WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                            if (socket.State == WebSocketState.Open)
                            {
                                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"count\":0,\"mes\":\"" + d + "\"}"));
                                socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch { continue; }

                    startcount = count;
                    //var data2 = redisCli.Get<string>(RedisSortListKey);
                }
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
            int retsped =Convert.ToInt32( 35000 - (30000 * mlog));
            return retsped;
            //if (tcount > qcount)
            //{
            //    var logs = Math.Log((tcount - qcount)/10, maxmcount);
            //    return Convert.ToInt32()
            //}
            //else
            //{
               
            //    return speed + (10000 * 10 / mycount) * scount;
            //}
            //int speed = 30000;
            //int maxmcount = 800;
            //if (tcount > qcount)
            //{
            //    int shijimcount = 0;
            //    if (tcount / 10 > 800)
            //        shijimcount = maxmcount;
            //    else
            //        shijimcount = tcount / 10;
            //    return speed - (10000 / 800) * shijimcount;
            //}
            //else
            //{
            //    int scount = mycount - qcount;
            //    return speed + (10000 * 10 / mycount) * scount;
            //}
        }
    }
}
