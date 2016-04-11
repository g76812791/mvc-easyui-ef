using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;
using KbaseData;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class DownloadController : BaseController<downfile>
    {
        //
        // GET: /Back/Download/

        public DownloadController()
        {
            base.fileds = new[] {"FileName", "FilePath", "UpDateTime"};
        }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="en">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(downfile en)
        {
            listWhere = ExpressExtens.True<downfile>();
            if (!string.IsNullOrEmpty(en.FileName))
            {
                listWhere = listWhere.And(q => q.FileName.Contains(en.FileName));
            }
            if (!en.beginDate.IsEmptyDate())
            {
                listWhere = listWhere.And(q => q.UpDateTime >= en.beginDate);
            }
            if (!en.endDate.IsEmptyDate())
            {
                en.endDate = en.endDate.AddDays(1);
                listWhere = listWhere.And(q => q.UpDateTime < en.endDate);
            }
            listOrder = "Id";
            return base.GetList(en);
        }


    }
}
