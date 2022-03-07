using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Repositories;
using Project.Domain;

namespace Project.EntityFrameworkCore.Repositories
{

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly DbContext Context;
        protected DbSet<TEntity> Entity;
        public Repository(DbContext context)
        {
            this.Context = context;
            Entity = Context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specs = null)
        {
            if(specs != null)
            {
                return await Context.Set<TEntity>()
                    .Where(specs.ToExpression())
                    .ToListAsync();
            }
            else
            {
                return await Context.Set<TEntity>()
                    .ToListAsync();
            }
        }
        protected IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual async ValueTask<TEntity> GetByIdAsync(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
