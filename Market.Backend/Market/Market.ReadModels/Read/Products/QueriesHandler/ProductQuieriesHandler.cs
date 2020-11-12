using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Repository;
using Market.ReadModels.Models;
using Market.ReadModels.Read.Products.Queries;

namespace Market.ReadModels.Read.Products.QueriesHandler
{
    public class ProductQueriesHandler :
        IQueryHandler<GetAllProducts,IReadOnlyList<ProductReadModel>>,
        IQueryHandler<GetProducts,IReadOnlyList<ProductReadModel>>
    {
        private readonly IReadModelRepository<ProductReadModel,Guid> _repo;

        public ProductQueriesHandler(IReadModelRepository<ProductReadModel,Guid> repo)
        {
            _repo = repo;
        }

        public Task<IReadOnlyList<ProductReadModel>> HandleAsync(GetAllProducts query, CancellationToken cancellationToken = default)
        {
            return _repo.QueryListAsync(null, 0, int.MaxValue, cancellationToken);
        }

        public Task<IReadOnlyList<ProductReadModel>> HandleAsync(GetProducts query, CancellationToken cancellationToken = new CancellationToken())
        {
            return _repo.QueryListAsync(page: query.Page, pageSize: query.PageSize,
                cancellationToken: cancellationToken);
        }
    }
}
