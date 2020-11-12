using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Repository;
using CoreTests.ReadModels.Query;

namespace CoreTests.ReadModels.QueryHandlers
{
    public class ProductQueriesHandler : IQueryHandler<GetAllProductQuery,IReadOnlyList<ProductReadModel>>
    {
        private readonly IReadModelRepository<ProductReadModel,Guid> _repo;

        public ProductQueriesHandler(IReadModelRepository<ProductReadModel,Guid> repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<ProductReadModel>> HandleAsync(GetAllProductQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _repo.QueryListAsync(null, 0, 20, cancellationToken);
        }
    }
}
