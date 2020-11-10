using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Commands;
using Core.Repository;
using CoreTests.Aggregates;
using CoreTests.Commands;

namespace CoreTests.CommandHandlers
{
    public class ProductCommandHandlers :
        ICommandHandler<CreateProductCommand>,
        ICommandHandler<ChangeProductNameCommand>
    {
        private readonly IAggregateRepository<ProductAggregate, Guid> _repo;
        public static bool IsHandled;
        public ProductCommandHandlers(IAggregateRepository<ProductAggregate,Guid> repo)
        {
            _repo = repo;
        }
        public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregate= new ProductAggregate(command);
            IsHandled = true;
            await _repo.SaveAsync(aggregate,cancellationToken);
        }

        //Todo changeProductNameCommand Handler
        public Task HandleAsync(ChangeProductNameCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }
    }
}
