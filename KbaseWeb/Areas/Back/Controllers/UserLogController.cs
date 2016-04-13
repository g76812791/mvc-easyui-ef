using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Entity;
using KbaseData;
using KbaseWeb.Areas.Back.Models;
using  Comm;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class UserLogController : BaseController<loginlog>
    {
        //
        // GET: /Back/UserLog/
        LoginLogDal logdal= new LoginLogDal();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取日志分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="p">第页</param>
        /// <returns></returns>
        public  ActionResult GetListView(view_loginlog p)
        {
            try
            {
                Expression<Func<view_loginlog, bool>> where = ExpressExtens.True<view_loginlog>();
                if (!string.IsNullOrEmpty(p.LoginName))
                {
                   where = where.And((q) => q.LoginName.Contains(p.LoginName));
                }
                if (!p.beginDate.IsEmptyDate())
                {
                    where = where.And((q) => q.LogTime >= p.beginDate);
                }
                if (!p.endDate.IsEmptyDate())
                {
                    p.endDate = p.endDate.AddDays(1);
                    where = where.And((q) => q.LogTime < p.endDate);
                }
                var list = logdal.GetList(p.rows, p.page, out total, where);
                return DateJson(new { total = total, rows = list });
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }


        
    }
}
