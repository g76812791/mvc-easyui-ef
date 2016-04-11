using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Comm;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class AnLiViewController : BaseController<view_anlidetails>
    {
        //
        // GET: /Back/AnLiView/
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="en">第页</param>
        /// <returns></returns>
        public override ActionResult GetList(view_anlidetails en)
        {
            listWhere = ExpressExtens.True<view_anlidetails>();
            if (!string.IsNullOrEmpty(en.Title))
            {
                listWhere = listWhere.And(q => q.Title.Contains(en.Title.Trim()));
            }
            en.AId= en.AId ?? 0;
            if (en.AId!=0)
            {
                listWhere = listWhere.And(q=>q.AId==en.AId);
            }
            listOrder = "AId";
            return base.GetList(en);
        }


    }
}
