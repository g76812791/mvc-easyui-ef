using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Entity;
using KbaseWeb.Filters;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class MenueController : BaseController<menue>
    {

        public MenueController()
        {
            base.fileds = new[] { "Icon", "MenueName", "Url", "OrderNum" };
        }
        [CheckAc(OpName = "add")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTree()
        {
            List<Tree> trees = new List<Tree>();
            List<menue> all = dal.GetListTopN(q => true, "Id", true, 0).ToList();
            List<menue> roots = all.Where(q => q.ParentId == 0).ToList();
            //菜单表转成easyui tree格式
            foreach (var root in roots)
            {
                trees.Add(GetDiGuiTree(root, all));
            }
            return Json(trees);
        }

        /// <summary>
        /// 递归获取数据
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public Tree GetDiGuiTree(menue en, List<menue> all)
        {
            Tree thisNode = ConvertEnToNode(en);
            //获取ParentId等于Id的子节点
            var list = all.Where(q => q.ParentId == en.Id).OrderBy(q => q.OrderNum).ToList();
            if (list.Count > 0)
            {
                thisNode.children = new List<Tree>();
                foreach (var item in list)
                {
                    //递归子节点
                    thisNode.children.Add(GetDiGuiTree(item, all));
                }
            }
            return thisNode;
        }

        public override ActionResult Add(menue en)
        {
            try
            {
                dal.Add(en);
                Tree thisNode = ConvertEnToNode(en); ;
                return Success(thisNode);
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        /// <summary>
        /// menue 转换成 tree格式数据
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public Tree ConvertEnToNode(menue en)
        {
            Tree Node = new Tree()
            {
                id = en.Id,
                text = en.MenueName,
                state = "open",
                Checked = false,
                attributes = new { Level = en.Level, Url = en.Url, Icon = en.Icon, OrderNum = en.OrderNum }
            };
            return Node;
        }

        public virtual ActionResult DeleteTree(string id)
        {
            try
            {
                var rootid = new MySqlParameter
                {
                    ParameterName = "?rootid",
                    Value = id,
                };
                var back = new MySqlParameter
                {
                    ParameterName = "back",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                MySqlParameter[] parm = { rootid, back };
                dal.ExecuteSqlNonQuery("sp_menue_delete", parm);
                string val = parm[1].Value.ToString();

                if (val == "0")
                {
                    return Success();
                }
                else
                {
                    return Error(new Exception(Tip.Error));
                }

            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

    }
}
