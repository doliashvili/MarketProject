using Market.Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Commands;
using Core.Repository;
using Market.Domain.Products.Entities;
using Market.Domain.Products.ValueObjects;

namespace Market.Application.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProductCommand>,
        ICommandHandler<DeleteProductCommand>
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

        public async Task HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken:cancellationToken);
            if (aggregateProduct==null)
            {
                throw new ArgumentNullException();
            }

            aggregateProduct.Delete(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }
    }
}
