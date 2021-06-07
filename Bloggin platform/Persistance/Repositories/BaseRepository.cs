using Bloggin_platform.Persistance.Context;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly BaseDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(BaseDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await DbSet.Where(condition).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}
