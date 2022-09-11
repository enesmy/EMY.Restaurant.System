using EMY.Restaurant.Core.Application.Repositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public WriteRepository(DbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<int> AddAsync(TEntity entity, Guid creater)
        {
            entity.CreatorID = creater;
            entity.LastUpdaterID = creater;
            await Table.AddAsync(entity);
            return await SaveChangesAsync(default);
        }

        public async Task<int> AddRangeAsync(IList<TEntity> entities, Guid creater)
        {
            foreach (var entity in entities)
            {
                entity.CreatorID = creater;
                entity.LastUpdaterID = creater;
            }
            await Table.AddRangeAsync(entities);
            return await SaveChangesAsync(default);
        }

        public async Task<int> RemoveAsync(TEntity entity, Guid remover)
        {
#if DEBUG
            Table.Remove(entity);
#else
            entity.IsDeleted = true;
            entity.DeleterID = remover;
            Table.Update(entity);
#endif
            return await SaveChangesAsync(default);
        }

        public Task<int> RemoveAsync(Guid id, Guid remover)
        {
            var entity = Table.Find(id);
            entity.DeleterID = remover;
            return RemoveAsync(entity, remover);
        }

        public async Task<int> RemoveRangeAsync(IList<Guid> IDs, Guid remover)
        {
            List<TEntity> entities = new List<TEntity>();
            foreach (var id in IDs)
            {
                var entity = await Table.FindAsync(id);
                entity.DeleterID = remover;
                entities.Add(entity);
            }
            return await RemoveRangeAsync(entities, remover);
        }

        public async Task<int> RemoveRangeAsync(IList<TEntity> entities, Guid remover)
        {


#if DEBUG
            Table.RemoveRange(entities);
            return await SaveChangesAsync(default);
#else

            entities.ToList().ForEach(o => { o.IsDeleted = true; o.DeleterID = remover; });
            Table.UpdateRange(entities);
            return await SaveChangesAsync(default);
#endif
        }

        public async Task<int> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate, Guid remover)
        {
            var entities = Table.Where(predicate);
            return await RemoveRangeAsync(await entities.ToListAsync(), remover);
        }

        public async Task<int> UpdateAsync(TEntity entity, Guid updater)
        {
            entity.LastUpdaterID = updater;
            Table.Update(entity);
            return await SaveChangesAsync(default);
        }

        public async Task<int> UpdateRangeAsync(IList<TEntity> entities, Guid updater)
        {
            foreach (var entity in entities)
            {
                entity.LastUpdaterID = updater;
            }

            Table.UpdateRange(entities);
            return await SaveChangesAsync(default);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
