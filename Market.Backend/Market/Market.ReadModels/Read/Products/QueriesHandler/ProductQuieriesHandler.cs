using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.Expression;
using Core.Queries;
using Core.Repository;
using Market.ReadModels.Models;
using Market.ReadModels.Read.Products.Queries;

namespace Market.ReadModels.Read.Products.QueriesHandler
{
    public class ProductQueriesHandler :
        IQueryHandler<GetAllProducts, IReadOnlyList<ProductReadModel>>,
        IQueryHandler<GetProducts, (IReadOnlyList<ProductReadModel> page, int pageCount)>,
        IQueryHandler<GetProductById, ProductReadModel>,
        IQueryHandler<GetAllProductCount, int>,
        IQueryHandler<GetFilteredProducts, (IReadOnlyList<ProductReadModel> page, int pageCount)>
    {
        private readonly IReadModelRepository<ProductReadModel, Guid> _repo;

        public ProductQueriesHandler(IReadModelRepository<ProductReadModel, Guid> repo)
        {
            _repo = repo;
        }

        public Task<IReadOnlyList<ProductReadModel>> HandleAsync(GetAllProducts query, CancellationToken cancellationToken = default)
        {
            return _repo.QueryListAsync(null, 1, int.MaxValue, cancellationToken);
        }

        public async Task<(IReadOnlyList<ProductReadModel> page, int pageCount)> HandleAsync(GetProducts query, CancellationToken cancellationToken = new CancellationToken())
        {
            var productsForPage = await _repo.QueryListAsync(page: query.Page ?? 1, pageSize: query.PageSize ?? 10,
                cancellationToken: cancellationToken);
            var countProducts = await _repo.CountAsync(cancellationToken);

            (IReadOnlyList<ProductReadModel> page, int pageCount) result = (productsForPage, countProducts);

            return result;
        }

        public Task<ProductReadModel> HandleAsync(GetProductById query, CancellationToken cancellationToken = default)
        {
            return _repo.GetByIdAsync(query.Id, cancellationToken);
        }

        public Task<int> HandleAsync(GetAllProductCount query, CancellationToken cancellationToken = default)
        {
            return _repo.CountAsync(cancellationToken);
        }

        public async Task<(IReadOnlyList<ProductReadModel> page, int pageCount)> HandleAsync(GetFilteredProducts query, CancellationToken cancellationToken = default)
        {
            var predicate = GetFilteredExpression(query);

            var productsForPage = await _repo.QueryListAsync(predicate, query.Page ?? 1, query.PageSize ?? 10, cancellationToken);
            var filteredCount = await _repo.CountAsync(predicate, cancellationToken);

            (IReadOnlyList<ProductReadModel> page, int pageCount) result = (productsForPage, filteredCount);
            return result;
        }

        private Expression<Func<ProductReadModel, bool>> GetFilteredExpression(GetFilteredProducts query)
        {
            Expression<Func<ProductReadModel, bool>> predicate =
                x => x.Price >= query.PriceFrom && x.Price <= query.PriceTo;

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                Expression<Func<ProductReadModel, bool>> nameExpression =
                    x => x.Name.ToLower().Contains(query.Name.ToLower());
                predicate = predicate.And(nameExpression);
            }

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                Expression<Func<ProductReadModel, bool>> brandExpression =
                    x => x.Brand.ToLower().Contains(query.Brand.ToLower());
                predicate = predicate.And(brandExpression);
            }

            if (!string.IsNullOrWhiteSpace(query.Color))
            {
                Expression<Func<ProductReadModel, bool>> colorExpression =
                    x => x.Color.ToLower().Contains(query.Color.ToLower());
                predicate = predicate.And(colorExpression);
            }

            if (!string.IsNullOrWhiteSpace(query.ProductType))
            {
                Expression<Func<ProductReadModel, bool>> productTypeExpression =
                    x => x.ProductType.ToLower().Contains(query.ProductType.ToLower());
                predicate = predicate.And(productTypeExpression);
            }

            if (!string.IsNullOrWhiteSpace(query.Size))
            {
                Expression<Func<ProductReadModel, bool>> sizeExpression =
                    x => x.Size.ToLower().Contains(query.Size.ToLower());
                predicate = predicate.And(sizeExpression);
            }

            if (query.ForBaby != null)
            {
                Expression<Func<ProductReadModel, bool>> forBabyExpression =
                    x => x.ForBaby == query.ForBaby;
                predicate = predicate.And(forBabyExpression);
            }

            if (query.Gender != null)
            {
                Expression<Func<ProductReadModel, bool>> genderExpression =
                    x => x.Gender == query.Gender;
                predicate = predicate.And(genderExpression);
            }

            return predicate;
        }
    }
}
