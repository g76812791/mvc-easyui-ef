using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;
using KbaseData;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class RoleController : BaseController<role>
    {
        public RoleController()
        {
            fileds = new[] { "Name", "Detail" };
        }

        public ActionResult Index()
        {
            return View();
        }

        public override ActionResult GetList(role en)
        {
            listWhere = ExpressExtens.True<role>();

            if (!string.IsNullOrEmpty(en.Name))
            {
                listWhere = listWhere.And(q => q.Name.Contains(en.Name.Trim()));
            }
            listOrder = "CreateTime";
            return base.GetList(en);

        }

        public ActionResult SaveRoleMenue(long Rid, string Mids)
        {
            BaseDal<rolemenue> rmDal = new BaseDal<rolemenue>();
            rolemenue rmModel = new rolemenue();
            try
            {
                rmDal.ExecSqlCommand(string.Format("delete from {0}  WHERE rid = {1}", "rolemenue", Rid));
                Mids.Split(',').ToList().ForEach(
                q =>
                {
                    rmModel = new rolemenue() { Rid = Rid, Mid = Convert.ToInt64(q) };
                    rmDal.Add(rmModel);
                }
                );
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

        public ActionResult GetRole()
        {
          //  base.UserId;

            BaseDal<view_userrole> daluDal= new BaseDal<view_userrole>();

            try
            {
                long uid = Convert.ToInt64(base.UserId);
                var list = daluDal.GetListTopN(q => q.Uid ==uid , "Id", true, 0);

                return DateJson(list);
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

        public ActionResult GetMenueByRid(long Rid)
        {
            BaseDal<rolemenue> rmDal = new BaseDal<rolemenue>();
            try
            {
                var list= rmDal.GetListTopN(q => q.Rid == Rid, "Id", true, 0);
                return DateJson(list);
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }
    }
}
