using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Repository;
using Market.ReadModels.Models;
using Market.ReadModels.Read.Queries.Products;

namespace Market.ReadModels.Read.Products.QueriesHandler
{
    public class ProductQueriesHandler :
        IQueryHandler<GetAllProducts,IReadOnlyList<ProductReadModel>>
    {
        private readonly IReadModelRepository<ProductReadModel> _repo;

        public ProductQueriesHandler(IReadModelRepository<ProductReadModel> repo)
        {
            _repo = repo;
        }

        public Task<IReadOnlyList<ProductReadModel>> HandleAsync(GetAllProducts query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
