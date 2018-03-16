using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace DBSql
{
    /// <summary>
    /// 数据持久化统一接口
    /// </summary>
    public interface IRepository<TEntity>
    {

        /// <summary>
        /// 根据查询条件返回任何对象类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);


        /// <summary>
        /// 动态获取列表数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IQueryable<dynamic> GetPagedDynamic(Common.SqlPageInfo condition, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<TEntity> GetQuery();

        /// <summary>
        /// 根据条件获取IQueryable
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>IQueryable</returns>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键属性得到实体
        /// </summary>
        /// <param name="objectId">主键属性值</param>
        /// <returns>实体</returns>
        TEntity Get(object objectId);

        /// <summary>
        /// 根据条件获取唯一的实体
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件获取第一条实体
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体列表</returns>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件获取分页列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="condition">条件</param>
        /// <returns>实体分页列表</returns>
        IQueryable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, Common.SqlPageInfo condition);


        /// <summary>
        /// 根据条件获取分页列表
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="condition">条件</param>
        /// <returns>实体分页列表</returns>
        IQueryable<TEntity> GetPagedList(Common.SqlPageInfo condition);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns>总数</returns>
        int Count();

        /// <summary>
        /// 根据条件获取总数
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>总数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
