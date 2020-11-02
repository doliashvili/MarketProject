using System;
using Core.Events.DomainEvents;
using Core.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Tests.CarApp.DomainObjects;
using EventStore.EventStoreDb.Helpers;

namespace Core.Tests.TestImplementations
{
    public class InMemoryEventStore<TA, TId> : IEventStore<TA, TId> 
        where TA: IAggregateRoot<TId> 
        where TId: IComparable, IEquatable<TId>
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1,1);
        private static List<IDomainEvent<TId>> _events = new List<IDomainEvent<TId>>();
        

        public async Task AppendAsync(IEnumerable<IDomainEvent<TId>> domainEvents, CancellationToken cancellationToken = default)
        {
            try
            {
                await Semaphore.WaitAsync(cancellationToken);
                foreach (var domainEvent in domainEvents)
                {
                    _events.Add(domainEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public Task<TA> RestoreAsync(TId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
