using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Entity;
using KbaseData;
using KbaseWeb.Areas.Back.Controllers;
using KbaseWeb.Areas.Back.Models;
using Comm;
namespace KbaseWeb.Areas.Back.Controllers
{
    public class UserController : BaseController<user>
    {
        BaseDal<userrole> urDal = new BaseDal<userrole>();
        // GET: Back/User/GetUsers
        public ActionResult Index()
        {
            return View();
        }

        public override ActionResult Add(user u)
        {
            //有条件添加 判断 用户名是否存在
            Expression<Func<user, bool>> where = q => q.LoginName == u.LoginName.Trim();
            string Rid = Request["Rid"];

            try
            {
                if (dal.Exist(where))
                {
                    return Error(new Exception("该记录已存在"));
                }
                else
                {
                    long uid = dal.Add(u).Id;

                    Rid.Split(',').ToList().ForEach(
                        q =>
                        {
                            userrole urole = new userrole() { Uid = uid, Rid = Convert.ToInt64(q) };
                            urDal.Add(urole);
                        }
                        );

                    return Success();
                }
            }
            catch (Exception e)
            {
                return Error(e);
            }



            //return AddExpress(u, where);
        }

        public override ActionResult Update(user u)
        {
            string Rid = Request["Rid"];
            fileds = new[] { "LoginName", "Pwd" };
            Expression<Func<user, bool>> where = q => q.Id != u.Id;
            where = where.And(q => q.LoginName == u.LoginName.Trim());

            try
            {
                if (dal.Exist(where))
                {
                    return Error(new Exception("该记录已存在"));
                }
                else
                {
                    dal.Update(u, fileds);
                   
                    urDal.ExecSqlCommand(string.Format("delete from {0}  WHERE uid = {1}","userrole", u.Id));

                    Rid.Split(',').ToList().ForEach(
                     q =>
                     {
                         userrole urole = new userrole() { Uid = u.Id, Rid = Convert.ToInt64(q) };
                         urDal.Add(urole);
                     }
                     );

                    return Success();
                }
            }
            catch (Exception e)
            {
                return Error(e);
            }
            // return UpdateExpress(u, fileds, where);



        }

        public ActionResult GetCombo()
        {
            try
            {
                BaseDal<role> rdal = new BaseDal<role>();
                var list = rdal.GetListTopN(q => true, "Id", true, 0);
                return DateJson(list);
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }



        public override ActionResult Delete(string ids)
        {
            try
            {
                dal.Delete(ids);
                urDal.ExecSqlCommand(string.Format("delete from {0}  WHERE uid in({1})", "userrole", ids));
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

    }
}
