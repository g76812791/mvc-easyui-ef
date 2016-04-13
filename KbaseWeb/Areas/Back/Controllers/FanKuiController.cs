using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class FanKuiController : BaseController<fankui>
    {

        // GET: /Back/FanKui/
        public FanKuiController()
        {
            base.fileds = new[] { "Name", "Phone", "QQ", "Email", "Context", "UpdateTime", "Flag", "Remark" };
        }

        public ActionResult Index()
        {
            return View();
        }

        [NoCompress]
        public override ActionResult Add(fankui en)
        {
            en.Flag = 0;
            return base.Add(en);
        }

        public override ActionResult Update(fankui en)
        {
            en.Flag = 1;
            en.UpdateTime = DateTime.Now;
            return base.Update(en);
        }

        //
        // GET: /Back/AnLiView/
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="en">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(fankui en)
        {
            listWhere = ExpressExtens.True<fankui>();
           
            if (en.Flag != -1&&en.Flag!=null)
            {
                listWhere = listWhere.And(q => q.Flag == en.Flag);
            }

            if (!en.beginDate.IsEmptyDate())
            {
                listWhere = listWhere.And(q => q.CreateTime >= en.beginDate);
            }
            if (!en.endDate.IsEmptyDate())
            {
                en.endDate = en.endDate.AddDays(1);
                listWhere = listWhere.And(q => q.CreateTime < en.endDate);
            }
            listOrder = "CreateTime";
            ListIsAsc = false;
            return base.GetList(en);
        }

    }
}
