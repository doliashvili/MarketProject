using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Repository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using ReadModelRepository.RavenDb.abstraction;

namespace ReadModelRepository.RavenDb.implementation
{
    /// <summary>
    /// IReadModelRepository implementation for ravenDb
    /// </summary>
    /// <typeparam name="TReadModel"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class RavenReadModelRepository<TReadModel> : IReadModelRepository<TReadModel>, IDisposable
        where TReadModel: IReadModel
    {
        private readonly IDocumentStore _db;
        private IAsyncDocumentSession _session = null;

        private IAsyncDocumentSession Session
        {
            get { return _session ??= _db.OpenAsyncSession(); }
        }


        public RavenReadModelRepository(IRavenConnectionWrapper ravenConnectionWrapper)
        {
            _db = ravenConnectionWrapper.Store;
        }


        private Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if(null == _session)
                throw new NullReferenceException(nameof(Session));
            return _session.SaveChangesAsync(cancellationToken);
        }


        public async Task<TReadModel> WriteAsync(TReadModel readModel, CancellationToken cancellationToken = default)
        {
            await Session.StoreAsync(readModel, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return readModel;
        }

        public async Task<TReadModel> UpdateAsync(TReadModel updatedObj, CancellationToken cancellationToken = default)
        {
            await Session.StoreAsync(updatedObj, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return updatedObj;
        }


        public async Task<TReadModel> UpdateAsync(string id, Action<TReadModel> action, CancellationToken cancellationToken = default)
        {
            var obj = await Session.LoadAsync<TReadModel>(id, cancellationToken);
            if (obj == null)
                return default;
            action(obj);
            await SaveChangesAsync(cancellationToken);
            return obj;
        }


        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            Session.Delete(id);
            await Session.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAsync(Expression<Func<TReadModel, bool>> selector, 
            CancellationToken cancellationToken = default)
        {
            var objects = await Session.Query<TReadModel>().Where(selector).ToListAsync(cancellationToken);
            objects?.ForEach(x=> Session.Delete(x));
            await Session.SaveChangesAsync(cancellationToken);
        }

        public Task<TReadModel> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Session.LoadAsync<TReadModel>(id, cancellationToken);
        }


        public Task<TReadModel> GetFirstOrDefaultAsync(Expression<Func<TReadModel, bool>> selector, CancellationToken cancellationToken = default)
        {
            return Session.Query<TReadModel>().FirstOrDefaultAsync(selector, cancellationToken);
        }


        public async Task<IReadOnlyList<TReadModel>> QueryListAsync(Expression<Func<TReadModel, bool>> filter = null, int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return filter == null ? await  Session.Query<TReadModel>().ToListAsync(cancellationToken) 
                : await Session.Query<TReadModel>()
                .Where(filter)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }


        public IQueryable<TReadModel> GetQueryable()
        {
            return Session.Query<TReadModel>().AsQueryable();
        }


        public IQueryable<TReadModel> GetQueryable(Expression<Func<TReadModel, bool>> filter)
        {
            return Session.Query<TReadModel>().AsQueryable().Where(filter);
        }


        public async Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            return await Session.Query<TReadModel>().CountAsync(cancellationToken);
        }


        public async Task<long> CountAsync(Expression<Func<TReadModel, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await Session.Query<TReadModel>().CountAsync(expression, cancellationToken);
        }


        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}
