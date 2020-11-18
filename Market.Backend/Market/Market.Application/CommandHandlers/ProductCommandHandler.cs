using Market.Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Commands;
using Core.Repository;
using Market.Domain.Products.Entities;
using Market.Domain.Products.Events;
using Market.Domain.Products.Exceptions;
using Market.Domain.Products.ValueObjects;

namespace Market.Application.CommandHandlers
{
    public class ProductCommandHandler :
        ICommandHandler<CreateProductCommand>,
        ICommandHandler<DeleteProductCommand>,
        ICommandHandler<ChangeProductNameCommand>,
        ICommandHandler<ChangeProductPriceCommand>
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
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.Delete(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductNameCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductName(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductPriceCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }
         
            aggregateProduct.ChangeProductPrice(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }
    }
}
