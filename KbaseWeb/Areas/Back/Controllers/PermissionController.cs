using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using KbaseData;

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

        public ActionResult GetPermissionByRid(long Rid)
        {
            BaseDal<view_rolemenue> rmDal = new BaseDal<view_rolemenue>();
            try
            {
                var list = rmDal.GetListTopN(q => q.Rid == Rid, "Id", true, 0).Select(q=>q.Mid).ToList();

                var listpermission= dal.GetListTopN(q=>true, "Id", true, 0).ToList();

                listpermission = listpermission.Where(q => list.Contains(q.Mid)).ToList();

                return DateJson(listpermission);
            }
            catch (Exception e)
            {
                return Error(e);
            }
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
