using EMY.Restaurant.Core.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMY.Restaurant.Core.Application.Repositories
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll(bool tracking = true);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
        TEntity? GetById(Guid id);
        TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    }
}
