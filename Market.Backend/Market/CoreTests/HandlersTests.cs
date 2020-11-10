using Core.InternalEventSystem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;
using CoreTests.CommandHandlers;
using CoreTests.Commands;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreTests
{
    public class HandlersTests
    {
        private readonly IServiceProvider _provider;
        private readonly ICommandSender _commandPublisher;
        private readonly IInternalEventPublisher _internalEventPublisher;

        public HandlersTests()
        {
            _provider = DependencyInjection.GetServiceProvider();
            _commandPublisher = _provider.GetRequiredService<ICommandSender>();
            _internalEventPublisher = _provider.GetRequiredService<IInternalEventPublisher>();
        }

        [Fact]
        public async Task HandleCreateProductCommand()
        {
            var command= new CreateProductCommand("Zamtris Kurtka",
                new CommandMeta(){ CommandId=Guid.NewGuid(),CorrelationId =Guid.NewGuid()});

            await _commandPublisher.SendAsync(command);

            Assert.True(ProductCommandHandlers.IsHandled);
        }


    }
}
