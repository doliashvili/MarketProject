using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.InternalEventSystem;
using Core.Repository;
using Market.Domain.Products.Events;
using Market.ReadModels.Models;

namespace Market.ReadModels.Write.InternalEventHandler
{
    public class ProductDomainEventProcessor :
        IInternalEventHandler<CreatedProductEvent>,
        IInternalEventHandler<DeletedProductEvent>,
        IInternalEventHandler<ChangedProductNameEvent>,
        IInternalEventHandler<ChangedProductPriceEvent>

    {
        private readonly IReadModelRepository<ProductReadModel, Guid> _repo;

        public ProductDomainEventProcessor(IReadModelRepository<ProductReadModel, Guid> repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync(CreatedProductEvent @event, CancellationToken cancellationToken = default)
        {
            var productModel = new ProductReadModel()
            {
                Id = @event.AggregateId,
                Version = @event.Version,
                Price = @event.Price,
                Color = @event.Color,
                Brand = @event.Brand,
                ProductType = @event.ProductType,
                Weight = @event.Weight,
                Name = @event.Name,
                Description = @event.Description,
                Gender = @event.Gender,
                ForBaby = @event.ForBaby,
                Size = @event.Size,
                Discount = @event.Discount,
                CreateTime = @event.CreateTime,
                Expiration = @event.Expiration,
                Images = @event.Images,
            };

            await _repo.WriteAsync(productModel, cancellationToken);
        }

        public async Task HandleAsync(DeletedProductEvent @event, CancellationToken cancellationToken = default)
        {
            await _repo.DeleteAsync(@event.AggregateId,cancellationToken);
        }

        public async Task HandleAsync(ChangedProductNameEvent @event, CancellationToken cancellationToken = default)
        {
            await _repo.UpdateAsync(@event.AggregateId, x => x.Name = @event.Name,cancellationToken);
        }

        public async Task HandleAsync(ChangedProductPriceEvent @event, CancellationToken cancellationToken = default)
        {
            await _repo.UpdateAsync(@event.AggregateId, x => x.Price = @event.Price,cancellationToken);
        }
    }
}
