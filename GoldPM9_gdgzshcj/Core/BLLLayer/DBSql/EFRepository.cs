using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using EntityFramework.Extensions;
using EntityFramework.Caching;
using System.Linq.Dynamic;
using Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Collections;
namespace DBSql
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected DbContext _dbContext;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// 预热EntityFramework
        /// </summary>
        public void AutoEntityFramework()
        {
            using (DbContext)
            {
                var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }

        public void DbContextRepository(IUnitOfWork work, DbContext db)
        {
            _dbContext = db;
            _unitOfWork = work;
        }

        public void DbContextRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new DAL.JQPM9_DefaultContext();
                    //_dbContext.Configuration.AutoDetectChangesEnabled = false;
                    //_dbContext.Configuration.ValidateOnSaveEnabled = false;
                    //_dbContext.Configuration.LazyLoadingEnabled = false;
                    //_dbContext.Configuration.ProxyCreationEnabled = false;
                }
                return _dbContext;
            }
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork(this.DbContext)); }
        }

        #region Implementation of IRepository<TEntity>


        /// <summary>
        /// 根据查询条件返回任何对象类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return Query<T>(queryMethod);
        }


        /// <summary>
        /// 动态获取列表数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<dynamic> GetPagedDynamic(Common.SqlPageInfo condition, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = GetQuery().AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (!String.IsNullOrEmpty(condition.Predicate))
            {
                query = query.Where(condition.Predicate, condition.PredicateValue);
            }
            if (!String.IsNullOrEmpty(condition.SelectOrder))
            {
                query = query.OrderBy(condition.SelectOrder);
            }
            if (!condition.ToPageData)
            {
                int PageIndex = condition.PageIndex;
                int PageSize = condition.PageSize;

                if (PageIndex < 1)
                    PageIndex = 1;
                int Size = PageSize == 0 ? 10 : PageSize;
                var itemIndex = (PageIndex - 1) * PageSize;

                condition.PageTotleRowCount = query.FutureCount();
                query = query.Skip(itemIndex).Take(PageSize);
            }
            else
                condition.PageTotleRowCount = 0;
            if (!String.IsNullOrEmpty(condition.SelectField))
            {
                return query.Select(condition.SelectField) as IQueryable<dynamic>;
            }
            return query;
        }

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<TEntity> GetQuery()
        {
            return DbContext.Set<TEntity>();
        }

        /// <summary>
        /// 根据条件获取IQueryable
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>IQueryable</returns>
        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 根据主键属性得到实体
        /// </summary>
        /// <param name="objectId">主键属性值</param>
        /// <returns>实体</returns>
        public TEntity Get(object objectId)
        {
            return DbContext.Set<TEntity>().Find(objectId);
        }

        /// <summary>
        /// 根据主键属性得到实体
        /// </summary>
        /// <param name="objectId">主键属性值</param>
        /// <returns>实体</returns>
        public TEntity GetOrDefault(object objectId)
        {
            var model = Get(objectId);
            if (model == null)
            {
                model = new TEntity();
            }
            return model;
        }

        /// <summary>
        /// 根据条件获取唯一的实体
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体</returns>
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 根据条件获取第一条实体
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体列表</returns>
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="FromCacheMin">缓存时间</param>
        /// <returns>实体列表</returns>
        ///
        public IEnumerable<TEntity> GetListFromCahe(Expression<Func<TEntity, bool>> predicate, int CacheMin, string CacheName)
        {
            return DbContext.Set<TEntity>().Where(predicate).AsNoTracking().FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromMinutes(CacheMin)), tags: new[] { CacheName }).ToList();
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="CacheNameList"></param>
        public void EFCacheRemove(params string[] CacheNameList)
        {
            if (CacheNameList != null)
            {
                foreach (var CacheName in CacheNameList)
                {
                    if (String.IsNullOrEmpty(CacheName)) continue;
                    EntityFramework.Caching.CacheManager.Current.Expire(CacheName);
                }
            }

        }

        /// <summary>
        /// 根据条件获取分页列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="condition">条件</param>
        /// <returns>实体分页列表</returns>
        public IQueryable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, SqlPageInfo condition)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().Where(predicate).AsNoTracking();

            if (!String.IsNullOrEmpty(condition.Predicate))
            {
                query = query.Where(condition.Predicate, condition.PredicateValue);
            }

            if (!String.IsNullOrEmpty(condition.SelectOrder))
            {
                query = query.OrderBy(condition.SelectOrder);
            }
            if (!condition.ToPageData)
            {
                int pageIndex = condition.PageIndex;
                int pageSize = condition.PageSize;

                if (pageIndex < 1)
                    pageIndex = 1;
                int Size = pageSize == 0 ? 10 : pageSize;
                var itemIndex = (pageIndex - 1) * pageSize;

                var totalCount = query.FutureCount(); ;
                query = query.Skip(itemIndex).Take(pageSize);
                condition.PageTotleRowCount = totalCount;

            }
            return query;
        }


        public IQueryable<TEntity> GetPagedList(SqlPageInfo condition)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().AsNoTracking();

            if (!String.IsNullOrEmpty(condition.Predicate))
            {
                query = query.Where(condition.Predicate, condition.PredicateValue);
            }


            if (!String.IsNullOrEmpty(condition.SelectOrder))
            {
                query = query.OrderBy(condition.SelectOrder);
            }
            if (!condition.ToPageData)
            {
                int pageIndex = condition.PageIndex;
                int pageSize = condition.PageSize;

                if (pageIndex < 1)
                    pageIndex = 1;
                int Size = pageSize == 0 ? 10 : pageSize;
                var itemIndex = (pageIndex - 1) * pageSize;

                var totalCount = query.FutureCount(); ;
                query = query.Skip(itemIndex).Take(pageSize);
                condition.PageTotleRowCount = totalCount;

            }
            return query;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Edit(TEntity entity)
        {
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// 实体部分更新
        /// </summary>
        /// <param name="originalEmployee">需修改的实体</param>
        /// <param name="newEmployee">新的实体</param>
        public void Edit(TEntity originalEmployee, TEntity newEmployee)
        {
            if (DbContext.Entry(originalEmployee).State != EntityState.Unchanged)
                DbContext.Entry(originalEmployee).State = EntityState.Unchanged;
            DbContext.Entry(originalEmployee).CurrentValues.SetValues(newEmployee);
        }

        /// <summary>
        /// 批量修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Edit(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> value)
        {
            DbContext.Set<TEntity>().Where(predicate).Update(value);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).Delete();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns>总数</returns>
        public int Count()
        {
            return DbContext.Set<TEntity>().Count();
        }

        /// <summary>
        /// 根据条件获取总数
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>总数</returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Count(predicate);
        }

        #endregion


        #region TSQL To Database

        /// <summary>
        /// 执行SQL 返回影响行数
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string Sql, params System.Data.SqlClient.SqlParameter[] Parameter)
        {
            return DbContext.Database.ExecuteSqlCommand(Sql, Parameter);
        }

        /// <summary>
        /// 返回查询实例
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> SqlQuery(string Sql, params System.Data.SqlClient.SqlParameter[] Parameter)
        {
            return DbContext.Set<dynamic>().SqlQuery(Sql, Parameter);
        }

        #endregion
    }
}