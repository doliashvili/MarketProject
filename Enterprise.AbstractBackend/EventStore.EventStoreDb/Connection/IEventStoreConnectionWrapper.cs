using EventStore.ClientAPI;

namespace EventStore.EventStoreDb.Connection
{
    public interface IEventStoreConnectionWrapper
    {
        IEventStoreConnection Connection { get; }
        EventStoreConfig Configuration { get; }
    }
}
