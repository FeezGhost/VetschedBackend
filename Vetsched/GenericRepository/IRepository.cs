using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vetsched.Data;

namespace Loader.infrastructure.GenericRepository
{
    public interface IRepository<TEntity,TContext> where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid Id);
        Task<TEntity> AddAsync(TEntity entity, bool createNewId = true);
        Task<TEntity> AddDetachedAsync(TEntity entity, bool createNewId = true);
        Task<IList<TEntity>> AddAsync(IList<TEntity> entities, bool createNewId = true);
        TEntity AddSync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, bool isDetached = false);
        Task<List<TEntity>> UpdateAsync(List<TEntity> entities);
        Task<TEntity> DeleteAsync(Guid Id, string modifiedBy = "");
        Task<bool> DeleteMultipleAsync(List<Guid> Ids, string modifiedBy = "");
        Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> where, string modifiedBy = "");
        IQueryable<TEntity> GetWithInclude(
           Expression<Func<TEntity, bool>> predicate, params string[] include);
        TEntity GetOneDefaultWithInclude(
           Expression<Func<TEntity, bool>> predicate, params string[] include);
        Task<bool> Exists(object primaryKey);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> where);
        Task<(int, List<TEntity>)> GetListWithPagination(Expression<Func<TEntity, bool>> where, int PageNumber, int PageSize, int? OrderBy = 0);
        Task<(int, List<TEntity>)> GetAllWithPagination(int PageNumber, int PageSize, int? OrderBy = 0);
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate, int? OrderBy = 0);
        IQueryable<TEntity> GetManyIQueryable(Expression<Func<TEntity, bool>> where);
        Task<(int, IList<TEntity>)> GetPaginationWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize, int? OrderBy = 0, params string[] include);
        (IQueryable<TEntity>,int) GetWithIncludePaginatedQueryAble(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize, params string[] include);
        (IQueryable<TEntity>, int) GetWithIncludeQueryAbleWithoutOrder(Expression<Func<TEntity, bool>> predicate, params string[] include);
        (IQueryable<TEntity>, int) GetPaginatedQueryAble(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize);
    }
}
