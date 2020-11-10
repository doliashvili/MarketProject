using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;
using Core.Events.DomainEvents;
using Core.Repository;
using Core.Tests.CarApp.Commands;
using Core.Tests.CarApp.DomainObjects;
using Core.Tests.CarApp.Events;
using Xunit;

namespace Core.Tests
{
    public class EventStoreEntityTest 
    {
        private readonly IEventStore<CarAgr, Guid> _eventStore;

        public EventStoreEntityTest()
        {
             _eventStore = DependencyInjection.GetService<IEventStore<CarAgr, Guid>>();
        }

        [Fact]
        public async Task Add_Comand_Event_Handler_Async()
        {
            var command = new CreateCar(Guid.NewGuid(), "mercedes", 2,
                new CommandMeta(){CorrelationId = Guid.NewGuid(),UserId="geno"});
            var carAgr= new CarAgr(command);


            await _eventStore.AppendAsync(carAgr.GetUncommittedEvents());


            var RestoreAgr = await _eventStore.RestoreAsync(carAgr.Id);



            Assert.Equal(carAgr.Id,RestoreAgr.Id);
        }



    }
}
