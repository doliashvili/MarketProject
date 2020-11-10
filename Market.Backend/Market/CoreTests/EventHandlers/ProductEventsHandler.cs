using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.InternalEventSystem;
using Core.Repository;
using CoreTests.Events;
using CoreTests.ReadModels;

namespace CoreTests.EventHandlers
{
    public class ProductEventsHandler : IInternalEventHandler<CreatedProductEvent>
    {
        private readonly IReadModelRepository<ProductReadModel> _repo;
        public static bool IsHandled;
        public ProductEventsHandler(IReadModelRepository<ProductReadModel> repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync(CreatedProductEvent @event, CancellationToken cancellationToken = new CancellationToken())
        {
            var productReadModel = new ProductReadModel()
            {
                Id = @event.AggregateId.ToString(),
                Name = @event.Name,
            };
            IsHandled = true;
            await _repo.WriteAsync(productReadModel);
        }
    }
}
