using LindCore.Domain.Entities;
using LindCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindCore.Repositories.Memory
{
    /// <summary>
    /// Mock仓储
    /// 说明:测试时使用,数据持久化到运行时中
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MemoryRepository<TEntity> : IExtensionRepository<TEntity>
        where TEntity : EntityBase<int>
    {
        /// <summary>
        /// 内存数据库
        /// </summary>
        static Dictionary<string, List<TEntity>> dataBase = new Dictionary<string, List<TEntity>>();
        string tableName;
        public MemoryRepository()
        {
            tableName = typeof(TEntity).Name;
        }

        public void BulkDelete(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => { Delete(i); });
        }

        public void BulkInsert(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => { Insert(i); });
        }

        public void BulkInsert(IEnumerable<TEntity> item, bool isRemoveIdentity)
        {
            throw new NotImplementedException();
        }

        public void BulkUpdate(IEnumerable<TEntity> item, params string[] fieldParams)
        {
            item.ToList().ForEach(i => { Update(i); });
        }

        public void Delete(TEntity item)
        {
            if (dataBase[tableName] == null)
            {
                dataBase[tableName].Remove(dataBase[tableName].FirstOrDefault(i => i.Id == item.Id));
            }
        }

        public void Delete(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => { Delete(i); });
        }

        public TEntity Find(params object[] id)
        {
            return GetModel().FirstOrDefault(i => i.Id == Convert.ToInt32(id[0]));
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetModel()
        {
            return dataBase[tableName].AsQueryable();
        }

        public IQueryable<TEntity> GetModel(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderBy, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity item)
        {
            if (dataBase[tableName] == null)
                dataBase[tableName] = new List<TEntity>();
            dataBase[tableName].Add(item);
        }

        public void Insert(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => { Insert(i); });
        }

        public void SetDataContext(object db)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity item)
        {
            if (dataBase[tableName] != null)
            {
                var entity = dataBase[tableName].Find(i => i.Id == item.Id);
                entity = item;
            }
        }

        public void Update(IEnumerable<TEntity> item)
        {
            item.ToList().ForEach(i => { Update(i); });
        }
    }
}
