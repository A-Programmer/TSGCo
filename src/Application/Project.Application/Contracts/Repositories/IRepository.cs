using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.Domain;

namespace Project.Application.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        ValueTask<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specs = null);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
