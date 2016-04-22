using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class PermissionController : BaseController<permission>
    {
        //
        // GET: /Back/Permission/

        public PermissionController()
        {
            fileds = new[] {"Name","SmallName"};
        }

        public ActionResult GetListByMid(long Mid)
        {
            topWhere = q => q.Mid == Mid;
            return base.GetListTop();
        }


        public ActionResult AddMuBan(long Mid,string SmallName)
        {
            List<permission> list= new List<permission>
            {
                new permission(){Mid = Mid,Name="添加",SmallName = "Back."+SmallName+".Add"},
                new permission(){Mid = Mid,Name="删除",SmallName = "Back."+SmallName+".Delete"},
                new permission(){Mid = Mid,Name="修改",SmallName = "Back."+SmallName+".Edit"}
            };
            try
            {
                foreach (var l in list)
                {
                    dal.Add(l);
                }
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }
            
        }
    }
}
