using EMY.Restaurant.Core.Application.Repositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public ReadRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll(bool tracking = true) => (tracking ? Table : Table.AsNoTracking());

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true) => (tracking ? Table : Table.AsNoTracking()).Where(predicate);

        public TEntity? GetById(Guid id) => Table.Find(id);

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool tracking = true) => Table.FirstOrDefault(predicate);

        public async Task<TEntity?> GetByIdAsync(Guid id) => await Table.FindAsync(id);

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true) => await (tracking ? Table : Table.AsNoTracking()).FirstOrDefaultAsync(predicate);

    }
}
