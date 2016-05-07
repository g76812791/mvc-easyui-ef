using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Configuration;
using System.Threading.Tasks;
using ServiceStack.Redis;
using KCollector.Common;
using System.Threading;

namespace KWatcher.signalr
{
    //Hub的别名,方便前台调用
    [HubName("getMessage")]
    public class danmuHub1 : Hub
    {
        public static string RedisSortListKey = ConfigurationManager.AppSettings["RedisSortListKey"].ToString();
        public static string RedisListKey = ConfigurationManager.AppSettings["RedisListKey"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }
        //编写发送信息的方法
        public void SendMessage(string message)
        {
            //调用所有客户注册的本地的JS方法(broadcastMessage)
            Clients.All.broadcastMessage(message + DateTime.Now.ToString());
        }
        int mycount = 2000;
        private async Task ProcessWSChat()
        {
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

                        string str = "{\"count\":" + GetSpeed((count - startcount), qcount) + ",\"mes\":\"ok\"}";

                        foreach (var d in data)
                        {

                            Thread.Sleep(10000 / qcount);
                            ArraySegment<byte> buffer = new ArraySegment<byte>();
                            //WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                            string strs = "{\"count\":0,\"mes\":\"" + d + "\"}";
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
            int retsped = Convert.ToInt32(35000 - (30000 * mlog));
            return retsped;
        }
    }
}