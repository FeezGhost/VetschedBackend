using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vetsched.Data;
using Vetsched.Data.DBContexts;

namespace Loader.infrastructure.GenericRepository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : DbContext
    {
        private readonly VetschedContext _context;
        private readonly DbSet<TEntity> _entities;


        public Repository(VetschedContext context)
        {
            this._context = context;
            _entities = context.Set<TEntity>();

        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _entities.AsNoTracking().OrderByDescending(d => d.CreatedWhen).ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(Guid Id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<TEntity> AddAsync(TEntity entity, bool createNewId = true)
        {
            if(createNewId)
            {
                entity.Id = Guid.NewGuid();
            }
            //entity.CreatedBy = entity.ModifiedBy = _context.getUserId();
            entity.CreatedWhen = DateTime.UtcNow;
            entity.ModifiedWhen = DateTime.UtcNow;
            entity.Deleted = false;
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<TEntity> AddDetachedAsync(TEntity entity, bool createNewId = true)
        {
            if (createNewId)
            {
                entity.Id = Guid.NewGuid();
            }
            //entity.CreatedBy = entity.ModifiedBy = _context.getUserId();
            entity.CreatedWhen = DateTime.UtcNow;
            entity.ModifiedWhen = DateTime.UtcNow;
            entity.Deleted = false;
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry<TEntity>(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IList<TEntity>> AddAsync(IList<TEntity> entities, bool createNewId = true)
        {
            foreach (var item in entities)
            {
                if (createNewId)
                {
                    item.Id = Guid.NewGuid();
                    //item.CreatedBy = item.ModifiedBy = _context.getUserId();

                }
                item.CreatedWhen = DateTime.UtcNow;
                item.ModifiedWhen = DateTime.UtcNow;
                item.Deleted = false;
                _entities.Add(item);
            }
            await _context.SaveChangesAsync();
            return entities;
        }
        public TEntity AddSync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            //entity.CreatedBy = entity.ModifiedBy = _context.getUserId();
            entity.CreatedWhen = DateTime.UtcNow;
            entity.ModifiedWhen = DateTime.UtcNow;
            entity.Deleted = false;
             _entities.AddAsync(entity);
             _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, bool isDetached = false)
        {
            if (entity == null)
            {
                return null;
            }
            entity.ModifiedWhen = DateTime.UtcNow;
            //entity.ModifiedBy = _context.getUserId();
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property("CreatedBy").IsModified = false;
            _context.Entry(entity).Property("CreatedWhen").IsModified = false;
            await _context.SaveChangesAsync();
            if(isDetached == true)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<List<TEntity>> UpdateAsync(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            foreach (var item in entities)
            {
                //item.ModifiedBy = _context.getUserId();
                item.ModifiedWhen = DateTime.UtcNow;
                _context.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return entities;
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> DeleteAsync(Guid Id, string modifiedBy = "")
        {
            var data = await _entities.FindAsync(Id);
            if (data == null)
            {
                return null;
            }
            //data.ModifiedBy = _context.getUserId();
            if (modifiedBy != "")
            {
                data.ModifiedBy = modifiedBy;
            }
            data.ModifiedWhen = DateTime.UtcNow;
            data.Deleted = true;
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteMultipleAsync(List<Guid> Ids, string modifiedBy = "")
        {
            foreach(var id in Ids)
            {
                var data = await _entities.FindAsync(id);
                if (data == null)
                {
                    continue;
                }
                //data.ModifiedBy = _context.getUserId();
                if (modifiedBy != "")
                {
                    data.ModifiedBy = modifiedBy;
                }
                data.ModifiedWhen = DateTime.UtcNow;
                data.Deleted = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> where, string modifiedBy = "")
        {
            var data = await _entities.Where(where).ToListAsync();
            foreach (var item in data)
            {
                 //item.ModifiedBy = _context.getUserId();
                if (modifiedBy != "")
                {
                    item.ModifiedBy = modifiedBy;
                }
                item.ModifiedWhen = DateTime.UtcNow;
                item.Deleted = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Get list of entities with pagination
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="PageNumber">Page Number (should be greater than 0)</param>
        /// <param name="PageSize">Page Size (should be greate than 0)</param>
        /// <param name="OrderBy">0=>ascending 1=>descending</param>
        /// <returns></returns>
        public async Task<(int,List<TEntity>)> GetListWithPagination(Expression<Func<TEntity, bool>> where, int PageNumber, int PageSize, int? OrderBy = 0)
        {
            int skip = (PageNumber - 1) * PageSize;
            //int take = PageSize * PageNumber;
            int take = PageSize;
            var initial = _entities.AsNoTracking().Where(where).OrderByDescending(x => x.CreatedWhen);
            var total = initial.Count();
            if (OrderBy == 1)
            {
                return (total,await initial.Skip(skip).Take(take).ToListAsync());
            }
            else
            {
                return (total, await initial.Skip(skip).Take(take).ToListAsync());
            }
        }
        /// <summary>
        /// Get list of entities with pagination
        /// </summary>
        /// <param name="PageNumber">Page Number (should be greater than 0)</param>
        /// <param name="PageSize">Page Size (should be greate than 0)</param>
        /// <param name="OrderBy">0=>ascending 1=>descending</param>
        /// <returns></returns>
        public async Task<(int, List<TEntity>)> GetAllWithPagination(int PageNumber, int PageSize, int? OrderBy = 0)
        {
            int skip = (PageNumber - 1) * PageSize;
            int take = PageSize * PageNumber;
            var initial = _entities.AsNoTracking().OrderByDescending(x => x.CreatedWhen);
            var total = initial.Count();
            if (OrderBy == 1)
            {
                return (total, await initial.Skip(skip).Take(take).ToListAsync());
            }
            else
            {
                return (total, await initial.Skip(skip).Take(take).ToListAsync());
            }
        }

        public IQueryable<TEntity> GetWithInclude( Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            bool includeDeleted = include.Contains("deleted");
            include = include.Where(x => x != "deleted").ToArray();
            IQueryable<TEntity> query = _entities;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            if (includeDeleted)
            {
                return query.IgnoreQueryFilters().Where(predicate).OrderByDescending(d => d.CreatedWhen);
            }
            else
            {
                return query.AsNoTracking().Where(predicate).Where(x => x.Deleted == false).OrderByDescending(d => d.CreatedWhen);
            }
        }

        public (IQueryable<TEntity>, int) GetWithIncludePaginatedQueryAble(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize, params string[] include )
        {
            int skip = (PageNumber - 1) * PageSize;
            int take = PageSize * PageNumber; 
            IQueryable<TEntity> query = _entities;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            var total = query.AsNoTracking().Where(predicate).Count();
            return (query.AsNoTracking().Where(predicate).OrderByDescending(d => d.CreatedWhen).Skip(skip).Take(take), total);
        }
        public (IQueryable<TEntity>, int) GetPaginatedQueryAble(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize)
        {
            int skip = (PageNumber - 1) * PageSize;
            int take = PageSize * PageNumber;
            IQueryable<TEntity> query = _entities;
            var total = query.AsNoTracking().Where(predicate).Count();
            return (query.AsNoTracking().Where(predicate).OrderByDescending(d => d.CreatedWhen).Skip(skip).Take(take), total);
        }

        public (IQueryable<TEntity>, int) GetWithIncludeQueryAbleWithoutOrder(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = _entities;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            var total = query.AsNoTracking().Where(predicate).Count();
            return (query.AsNoTracking().Where(predicate), total);
        }

        public async Task<(int, IList<TEntity>)> GetPaginationWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, int PageNumber, int PageSize, int? OrderBy = 0, params string[] include)
        {
            int skip = (PageNumber - 1) * PageSize;
            int take = PageSize * PageNumber;
            IQueryable<TEntity> query = _entities;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            var initial = query.AsNoTracking().Where(predicate).OrderByDescending(d => d.CreatedWhen);

            var total = initial.Count();
            if (OrderBy == 1)
            {
                return (total, await initial.Skip(skip).Take(take).ToListAsync());
            }
            else
            {
                return (total, await initial.Skip(skip).Take(take).ToListAsync());
            }
        }

        

        public TEntity GetOneDefaultWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = _entities;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate).AsNoTracking().FirstOrDefault();
        }


        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(x=> x.Deleted == false).AnyAsync(predicate);
        }

        public async Task<bool> Exists(object primaryKey)
        {
            return await _entities.FindAsync(primaryKey) != null;
        }

        public async Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return await _entities.AsNoTracking().Where(where).Where(x => x.Deleted == false).OrderByDescending(d => d.CreatedWhen).ToListAsync();
        }


        public IQueryable<TEntity> GetManyIQueryable(Expression<Func<TEntity, bool>> where)
        {
            return _entities.AsNoTracking().Where(where).Where(x => x.Deleted == false).AsQueryable().OrderByDescending(d => d.CreatedWhen);
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate, int? OrderBy = 1)
        {
            if (OrderBy == 0)
            {
                return await _entities.AsNoTracking().Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedWhen).FirstOrDefaultAsync(predicate);
            }
            else
            {
                return await _entities.AsNoTracking().Where(x => x.Deleted == false).FirstOrDefaultAsync(predicate);
            }
        }

    }
}
