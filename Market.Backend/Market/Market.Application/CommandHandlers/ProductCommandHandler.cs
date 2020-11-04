using Market.Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Commands;
using Core.Repository;
using Market.Domain.Products.Entities;

namespace Market.Application.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IAggregateRepository<Product, Guid> _repo;

        public ProductCommandHandler(IAggregateRepository<Product, Guid> repo)
        {
            _repo = repo;
        }
        public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregateProduct = new Product(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }
    }
}
