using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Comm;
namespace Entity
{
   
    public class BaseRepository<T> where T : class
    {
        //实例化EF框架
        kbase_adminEntities db = new kbase_adminEntities();

        //添加
        public T AddEntities(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
            db.SaveChanges();
            return entity;
        }

        //修改全部字段
        public bool UpdateEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        //按需字段修改
        public bool UpdateEntities(T entity, string[] fileds)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Unchanged;
            foreach (var f in fileds)
            {
                db.Entry<T>(entity).Property(f).IsModified = true;
            }
            return db.SaveChanges() > 0;
        }
        //删除
        public bool DeleteEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleByIds(string ids)
        {
            string tableName = typeof(T).Name;
            return db.Database.ExecuteSqlCommand(string.Format("delete from {0}  WHERE id in ({1})",tableName,ids)) > 0;
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecSqlCommand(string sql)
        {
            return db.Database.ExecuteSqlCommand(sql) > 0;
        }

        //查询
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda)
        {
            return db.Set<T>().Where<T>(wherelambda).AsQueryable();
        }

        //查询top N topn==0 取得全部
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda, string order, bool isAsc, int topn)
        {
            if (topn==0)
            {
                return db.Set<T>().Where<T>(wherelambda).OrderBy(order, isAsc).AsQueryable();

            }
            else
            {
                return db.Set<T>().Where<T>(wherelambda).OrderBy(order, isAsc).Take(topn).AsQueryable();
            }
        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        public bool ExistEntitis(Expression<Func<T, bool>> wherelambda)
        {
             var tempData = db.Set<T>().Where<T>(wherelambda);
             return tempData.Count() > 0;
        }

        //分页
        public IQueryable<T> LoadPagerEntities(int pageSize, int pageIndex, out int total,
           Expression<Func<T, bool>> whereLambda, bool isAsc, string order)
        {
            var tempData = db.Set<T>().Where<T>(whereLambda);
            total = tempData.Count();
            tempData = tempData.OrderBy(order, isAsc).
                     Skip<T>(pageSize * (pageIndex - 1)).
                     Take<T>(pageSize).AsQueryable();
            return tempData.AsQueryable();
        }
    }
}
