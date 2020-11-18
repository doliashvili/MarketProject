using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace ReadModelRepository.MSSQL.Concrete
{
    public class RepositoryMSSQL<TReadModel, TId> : IReadModelRepository<TReadModel, TId>
    where TReadModel : class, IReadModel<TId>
    where TId : IComparable, IEquatable<TId>
    {
        private readonly AppDbContext _dbContext;

        public RepositoryMSSQL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TReadModel>().CountAsync(cancellationToken);
        }

        public async Task<long> CountAsync(Expression<Func<TReadModel, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TReadModel>().CountAsync(expression,cancellationToken);
        }

        public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TReadModel>().FirstOrDefaultAsync(x=> x.Id.Equals(id), cancellationToken);
            if (null != entity)
            {
                _dbContext.Set<TReadModel>().Remove(entity);
                await SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Expression<Func<TReadModel, bool>> selector, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TReadModel>().Where(selector).ToListAsync(cancellationToken);
            _dbContext.RemoveRange(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<TReadModel> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TReadModel>().FindAsync(id,cancellationToken);

            if (entity == null)
            {
                return default;
            }

            return entity;
        }

        public async Task<TReadModel> GetFirstOrDefaultAsync(Expression<Func<TReadModel, bool>> selector, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TReadModel>().FirstOrDefaultAsync(selector,cancellationToken);
            return entity;
        }

        public IQueryable<TReadModel> GetQueryable()
        {
            return _dbContext.Set<TReadModel>();
        }

        public IQueryable<TReadModel> GetQueryable(Expression<Func<TReadModel, bool>> filter)
        {
            return _dbContext.Set<TReadModel>().Where(filter);
        }

        public async Task<IReadOnlyList<TReadModel>> QueryListAsync(Expression<Func<TReadModel, bool>> filter = null, int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            if (filter!=null)
            {
                return await _dbContext.Set<TReadModel>()
                    ?.Where(filter)
                    ?.Skip(page * pageSize)
                    ?.Take(pageSize)
                    ?.AsNoTracking()
                    .ToListAsync(cancellationToken);
            }

            return await _dbContext.Set<TReadModel>()
                ?.Skip(page * pageSize)
                ?.Take(pageSize)
                ?.AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<TReadModel> UpdateAsync(TReadModel updatedObj, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TReadModel>().Update(updatedObj);

            await SaveChangesAsync(cancellationToken);

            return updatedObj;
        }

        public async Task<TReadModel> UpdateAsync(TId id, Action<TReadModel> action, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TReadModel>().FirstOrDefaultAsync(x=> x.Id.Equals(id), cancellationToken);
            if (entity == null)
            {
                return default;
            }

            action(entity);

            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TReadModel> WriteAsync(TReadModel readModel, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TReadModel>().AddAsync(readModel,cancellationToken);

            await SaveChangesAsync(cancellationToken);

            return readModel;
        }

        private string GetIncludedProperties(params Expression<Func<TReadModel, object>>[] includeProperties)
        {
            var builder = new StringBuilder();
            var entity = Activator.CreateInstance<TReadModel>();
            foreach (var property in includeProperties)
            {
                var propertyName = property.Compile()(entity).GetType().Name;
                builder.Append($"{propertyName},");
            }

            return builder.ToString().TrimEnd(',');
        }

        private async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
