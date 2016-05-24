using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;
using KbaseData;

namespace KbaseWeb.Areas.Back.Controllers
{
    public class HomeController : BaseController<user>
    {
        //
        // GET: /Back/Home/

        public ActionResult Index()
        {
            return View();
        }

        /*
                 var _menus = {
                    "menus": [
                         {
                             "menuid": "1", "icon": "icon-sys", "menuname": "下载管理",
                             "menus": [{ "menuname": "软件下载", "icon": "icon-set", "url": "/Back/Download/Index" }
                             ]
                         }, {
                             "menuid": "2", "icon": "icon-sys", "menuname": "内容管理",
                             "menus": [
                                       { "menuname": "经典案例类别", "icon": "icon-set", "url": "/Back/AnLiType/Index" },
                                       { "menuname": "经典案例列表", "icon": "icon-set", "url": "/Back/AnLi/Index" },
                                       { "menuname": "常见问题", "icon": "icon-set", "url": "/Back/Question/Index" },
                                       { "menuname": "问题反馈", "icon": "icon-set", "url": "/Back/FanKui/Index" }
                             ]
                         }, {
                             "menuid": "3", "icon": "icon-sys", "menuname": "首页管理",
                             "menus": [
                                     { "menuname": "导航列表", "icon": "icon-set", "url": "/Back/DaoHang/Index" },
                                     { "menuname": "首页说明列表", "icon": "icon-set", "url": "/Back/HomeInfo/Index" }
                             ]
                         }, {
                             "menuid": "3", "icon": "icon-sys", "menuname": "产品介绍管理",
                             "menus": [
                                     { "menuname": "产品介绍列表", "icon": "icon-set", "url": "/Back/Product/Index" },
                             ]
                         }, {
                             "menuid": "4", "icon": "icon-sys", "menuname": "系统管理",
                             "menus": [
                                     { "menuname": "用户管理", "icon": "icon-set", "url": "/Back/User/Index" },
                                     { "menuname": "菜单权限管理", "icon": "icon-set", "url": "/Back/Menue/Index" },
                                     { "menuname": "系统日志", "icon": "icon-set", "url": "/Back/UserLog/Index" }
                             ]
                         },
                    ]
                };*/


        public  ActionResult GetTree(long Rid)
        {
            //取得角色下的所有菜单
            BaseDal<rolemenue> rmDal = new BaseDal<rolemenue>();
            var list = rmDal.GetListTopN(q => q.Rid == Rid, "Id", true, 0).Select(q => q.Mid);
            BaseDal<menue> mdal = new BaseDal<menue>();
            List<HomeTree> trees = new List<HomeTree>();
            List<menue> all = mdal.GetListTopN(q => true, "Id", true, 0).ToList();
            //根节点
            menue root = all.Where(q => q.ParentId == 0).FirstOrDefault();
            all = all.Where(q => list.Contains(q.Id)).ToList();
            //一级子几点
            List<menue> _menus = all.Where(q => q.ParentId == root.Id).ToList();

            //菜单表转成 HomeTree 格式 添加集合
            foreach (var menue in _menus)
            {
                trees.Add(GetDiGuiTree(menue, all));
            }


            //权限加载设置
            List<string> permision = new List<string>() { "1", "2" };
            CacheHelper.SetCache(base.UserId, permision, 60);
            Func<List<string>> cc = () => new List<string>();
            List<string> m = CacheHelper.GetCache(base.UserId, cc);


            return Json(trees);
        }

        /// <summary>
        /// 递归获取数据
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public HomeTree GetDiGuiTree(menue en, List<menue> all)
        {
            HomeTree thisNode = ConvertEnToNode(en);
            //获取ParentId等于Id的子节点
            var list = all.Where(q => q.ParentId == en.Id).OrderBy(q => q.OrderNum).ToList();
            if (list.Count > 0)
            {
                thisNode.menus = new List<HomeTree>();
                foreach (var item in list)
                {
                    //递归子节点
                    thisNode.menus.Add(GetDiGuiTree(item, all));
                }
            }
            return thisNode;
        }



        /// <summary>
        /// menue 转换成 tree格式数据
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public HomeTree ConvertEnToNode(menue en)
        {
            HomeTree Node = new HomeTree()
            {
                menuid = en.Id,
                icon = en.Icon,
                menuname = en.MenueName,
                url = en.Url
            };
            return Node;
        }
    }
}
