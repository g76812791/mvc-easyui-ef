using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Comm;
using Entity;
using System.Web;
namespace KbaseData
{
    public class LoginLogDal 
    {
       /// <summary>
       /// 写入登陆日志
       /// </summary>
       /// <param name="u"></param>
        public void Add(user u)
        {
            QQWry.NET.QQWryLocator qqWry = new QQWry.NET.QQWryLocator(System.Web.HttpContext.Current.Server.MapPath(Config.GetValue<string>("qqwry")));
            BaseRepository<loginlog> dal= new BaseRepository<loginlog>();
            loginlog log = new loginlog();
            log.Uid = u.Id;
            log.Ip = ClientIp.GetClientIP();
            log.LogTime = DateTime.Now;
            log.Address = qqWry.Query(log.Ip).Country;
            dal.AddEntities(log);
        }
        /// <summary>
        /// 获取日志日志列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public IQueryable<view_loginlog> GetList(int pageSize, int pageIndex, out int total, Expression<Func<view_loginlog, bool>> where)
        {
            BaseRepository<view_loginlog> dal = new BaseRepository<view_loginlog>();
            IQueryable<view_loginlog> list =dal.LoadPagerEntities(pageSize, pageIndex, out total, where, false, "Id");
            return list;
        }
    }
}
