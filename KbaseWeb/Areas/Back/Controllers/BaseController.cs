using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Comm;
using Entity;
using KbaseWeb.Areas.Back.Models;
using KbaseWeb.Filters;
using KbaseData;

namespace KbaseWeb.Areas.Back.Controllers
{
    [IsLogin]
    public class BaseController<T> : Controller where T : class
    {
       public  BaseDal<T> dal = new BaseDal<T>();

        #region 登陆信息记录

        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string LoginName
        {
            get
            {
                try
                {
                    return CookieHelp.GetCookieValByKey("LoginName");
                }
                catch
                {
                    return null;
                }
            }
        }

        public string UserId
        {
            get
            {
                try
                {
                    return CookieHelp.GetCookieValByKey("UserId");
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region 查询数据格式处理

        public virtual ActionResult DateJson(object o)
        {
            return Content(JsonHelper.SerializeObject(o), "application/json"); //, System.Text.Encoding.UTF8
        }

        public virtual ActionResult Success()
        {
            return Json(new { Message = Tip.Success });
        }

        public virtual ActionResult Success(Object en)
        {
            return Json(new { Message = Tip.Success ,Context=en});
        }

        public virtual ActionResult Error(Exception e)
        {
            return Json(new { Message = Tip.Error, Context = e.Message });
        }

        #endregion

        #region 获取单个实体模型

        //获取by name or id 参数 q=>q.id=1
        public T GetOne(Expression<Func<T, bool>> wherelambda)
        {
            return dal.GetOne(wherelambda);
        }

        #endregion

        #region 删除控制器

        public virtual ActionResult Delete(string ids)
        {
            try
            {
                dal.Delete(ids);
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        #endregion

        #region 添加控制器

        /// <summary>
        /// 有条件的添加
        /// </summary>
        /// <param name="en"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual ActionResult AddExpress(T en, Expression<Func<T, bool>> where)
        {
            try
            {
                if (dal.Exist(where))
                {
                    return Error(new Exception("该记录已存在"));
                }
                else
                {
                    dal.Add(en);
                    return Success();
                }
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="en"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        //[CheckAc]
        public virtual ActionResult Add(T en)
        {
            try
            {
                dal.Add(en);
                return Success(en);
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        #endregion

        #region 修改控制器

        /// <summary>
        /// 有条件的修改
        /// </summary>
        /// <param name="en"></param>
        /// <param name="fileds"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual ActionResult UpdateExpress(T en, string[] fileds, Expression<Func<T, bool>> where)
        {
            try
            {
                if (dal.Exist(where))
                {
                    return Error(new Exception("该记录已存在"));
                }
                else
                {
                    dal.Update(en, fileds);
                    return Success();
                }
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }
        /// <summary>
        /// 修改指定字段
        /// </summary>
        protected string[] fileds { get; set; }

        public virtual ActionResult Update(T en)
        {
            try
            {
                if (fileds.Length == 0)
                {
                    dal.Update(en);
                }
                dal.Update(en, fileds);
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        #endregion

        #region 分页相关

        /// <summary>
        /// 页总数
        /// </summary>
        public int total;

        protected Expression<Func<T, bool>> listWhere { get; set; }

        protected string listOrder { get; set; }

        protected bool ListIsAsc = true;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="rows">页大小</param>
        /// <param name="p">第页</param>
        /// <returns></returns>
        public virtual ActionResult GetList(T en)
        {
            try
            {
                var list = dal.GetList((int)typeof(T).GetProperty("rows").GetValue(en, null),
                    (int)typeof(T).GetProperty("page").GetValue(en, null), out total, listWhere, listOrder,ListIsAsc);
                return DateJson(new { total = total, rows = list });
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        #endregion

        #region 根据条件获取列表 topn 或者所有数据

        protected Expression<Func<T, bool>> topWhere = ExpressExtens.True<T>();

        public virtual ActionResult GetListTop(string order = "Id", int topn = 0, bool isAsc = true)
        {
            try
            {
                var list = dal.GetListTopN(topWhere, order, isAsc, topn);
                return DateJson(list);
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

        #endregion

        #region 接受Form上下文自动装配

        protected PropertyInfo[] GetPropertyInfoArray(Type type)
        {
            PropertyInfo[] props = null;
            try
            {
                object obj = Activator.CreateInstance(type);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return props;
        }

        /// <summary>
        /// 自动装配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">HttpContext.Current.Request.Form</param>
        /// <returns></returns>
        public T PagerUpdateEn(NameValueCollection collection)
        {
            PropertyInfo[] propertyInfoList = GetPropertyInfoArray(typeof(T));
            object obj = Activator.CreateInstance(typeof(T), null); //创建指定类型实例
            foreach (string key in collection) //所有上下文的值
            {
                foreach (var PropertyInfo in propertyInfoList) //所有实体属性
                {
                    if (key.ToLower() == PropertyInfo.Name.ToLower())
                    {
                        PropertyInfo.SetValue(obj, collection[key], null); //给对象赋值
                    }
                }
            }
            return (T)obj;
        }

        #endregion

        #region 上传

        [HttpPost]
        public virtual ActionResult Upload()
        {
            string[] type = {".bmp", ".jpg", ".jpeg", ".png", ".gif"};
            List<long> list = new List<long>();
            try
            {
                BaseDal<image> idal = new BaseDal<image>();
                image en = new image();
                HttpFileCollectionBase collection = HttpContext.Request.Files;
                for (int i = 0; i < collection.Count; i++)
                {
                    HttpPostedFileBase singel = collection[i];
                    byte[] buff = new byte[singel.ContentLength];
                    singel.InputStream.Read(buff, 0, singel.ContentLength);
                    en.Name = singel.FileName;
                    en.Img = buff;
                    en = idal.Add(en);
                    en.ContentType = Path.GetExtension(singel.FileName);
                    if (type.Any(h => h == en.ContentType.ToLower()))
                    {
                        list.Add(en.ID);
                    }
                    else
                    {
                        return Error(new Exception("文件格式不支持"));
                    }
                }
                return Json(new {Message = Tip.Success, Ids = string.Join(",", list)});
            }
            catch (Exception e)
            {
                return Error(e);
            }
        }

        #endregion

        #region 处理状态执行sql

        public virtual ActionResult Handle(string ids, int Flag)
        {
            try
            {
                string sql = string.Format("update {0} set flag={1} where Id in ({2})", typeof (T).Name, Flag, ids);
                dal.ExecSqlCommand(sql);
                return Success();
            }
            catch (Exception e)
            {
                return Error(e);
            }

        }

        #endregion

    }

    #region 控制器过滤

    /// <summary>
    /// 不验证登陆身份
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NoCompress : Attribute
    {
        public NoCompress()
        {
        }
    }

    #endregion

    #region 消息提示

    /// <summary>
    /// ajax 消息提示
    /// </summary>
    public static class Tip
    {
        public const string Error = "Error";
        public const string Success = "Success";
    }

    #endregion

}
