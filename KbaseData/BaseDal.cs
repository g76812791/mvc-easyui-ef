using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entity;

namespace KbaseData
{
    public class BaseDal<T> : BaseRepository<T> where T:class 
    {
        //获取by name or id
        public T GetOne(Expression<Func<T, bool>> wherelambda)
        {
            IQueryable<T> list = LoadEntities(wherelambda);
            var l = list.FirstOrDefault();
            return l;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetList(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> where, string order)
        {
            IQueryable<T> list = LoadPagerEntities(pageSize, pageIndex, out total, where, true, order);
            return list;
        }

        public IQueryable<T> GetListTopN(Expression<Func<T, bool>> wherelambda, string order, bool isAsc, int topn)
        {
            IQueryable<T> list = LoadEntities(wherelambda, order, isAsc, topn);
            return list;
        }

        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool Update(T en , string[] fileds)
        {
            return UpdateEntities(en, fileds);
        }

        public bool Update(T en)
        {
            return UpdateEntities(en);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public T Add(T en)
        {
            return AddEntities(en);
        }

        /// <summary>
        /// 是否存在实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<T, bool>> where)
        {
            return ExistEntitis(where);
        }
        /// <summary>
        /// 删除实体 by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(string ids)
        {
            return DeleByIds(ids);
        }

    }
}
