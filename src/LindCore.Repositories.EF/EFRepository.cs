using LindCore.IRepositories;
using LindCore.LindLogger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LindCore.Repositories.EF
{
    /// <summary>
    /// EF进行持久化
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity> :
        IExtensionRepository<TEntity>
        where TEntity : class
    {
        #region Constructors
        /// <summary>
        /// 这个在IoC注入时走它
        /// </summary>
        //[Microsoft.Practices.Unity.InjectionConstructor]
        public EFRepository()
            : this(null)
        { }

        public EFRepository(DbContext db)
        {
            Db = db;
            this.DataPageSize = 10000;
        }
        #endregion

        /// <summary>
        /// EF数据上下文
        /// </summary>
        private DbContext Db;

        /// <summary>
        /// 提交到数据库
        /// </summary>
        protected virtual void SaveChanges()
        {
            try
            {

                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LoggerFactory.Logger_Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger_Error(ex);
                throw;
            }

        }

        #region IRepository<TEntity> 成员

        public void Delete(TEntity item)
        {
            if (item != null)
            {

                //物理删除
                Db.Set<TEntity>().Attach(item as TEntity);
                Db.Entry(item).State = EntityState.Deleted;
                Db.Set<TEntity>().Remove(item as TEntity);
                this.SaveChanges();
            }
        }
       
         public IQueryable<TEntity> GetModel()
        {
            return Db.Set<TEntity>();
        }
        public void Insert(TEntity item)
        {
            if (item != null)
            {
                Db.Entry<TEntity>(item as TEntity);
                Db.Set<TEntity>().Add(item as TEntity);
                this.SaveChanges();
            }

        }
        public void Update(TEntity item)
        {
            if (item != null)
            {
                Db.Set<TEntity>().Attach(item);
                Db.Entry(item).State = EntityState.Modified;
                this.SaveChanges();
            }
        }
        public void Insert(IEnumerable<TEntity> item)
        {
            foreach (var entity in item)
            {
                Db.Entry<TEntity>(entity as TEntity);
                Db.Set<TEntity>().Add(entity as TEntity);
            }
            this.SaveChanges();
        }


        public void Update(IEnumerable<TEntity> item)
        {
            #region 1个SQL连接,发N条语句，事务级
            foreach (var entity in item)
            {
                Db.Set<TEntity>().Attach(entity as TEntity);
                Db.Entry(entity).State = EntityState.Modified;
            }
            this.SaveChanges();
            #endregion
        }
        public void Delete(IEnumerable<TEntity> item)
        {
            foreach (var entity in item)
            {
                Db.Set<TEntity>().Attach(entity as TEntity);
                Db.Set<TEntity>().Remove(entity as TEntity);
                this.SaveChanges();
            }
        }
        public void SetDataContext(object db)
        {
            try
            {
                Db = (DbContext)db;
            }
            catch (Exception)
            {

                throw new ArgumentException("EF.SetDataContext要求上下文为DbContext类型");
            }

        }

        #endregion

        #region IExtensionRepository<TEntity> 成员

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetModel().Where(predicate);
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetModel().FirstOrDefault(predicate);
        }



        #endregion

        #region IOrderableRepository<TEntity> 成员

        public IQueryable<TEntity> GetModel(Action<IRepositories.IOrderable<TEntity>> orderBy)
        {
            var linq = new Orderable<TEntity>(GetModel());
            orderBy(linq);
            return linq.Queryable;
        }

        public IQueryable<TEntity> GetModel(Action<IRepositories.IOrderable<TEntity>> orderBy, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetModel().Where(predicate).AsQueryable();
            var linq = new Orderable<TEntity>(queryable);
            orderBy(linq);
            return linq.Queryable;
        }

        #endregion

        #region Fields
        /// <summary>
        /// 数据总数
        /// </summary>
        int _dataTotalCount = 0;

        /// <summary>
        /// 数据总页数
        /// </summary>
        int _dataTotalPages = 0;

        /// <summary>
        /// 数据页面大小（每次向数据库提交的记录数）
        /// 子类可以进行重写,或者在配置文件中进行设置
        /// </summary>
        protected virtual int DataPageSize { get; set; }
        #endregion

        #region Private Methods



     

        public void BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IEnumerable<TEntity> item)
        {
            throw new NotImplementedException();
        }

        public void BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            throw new NotImplementedException();
        }

        public void BulkDelete(IEnumerable<TEntity> item)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(params object[] id)
        {
            throw new NotImplementedException();
        }


        #endregion



    }
}
