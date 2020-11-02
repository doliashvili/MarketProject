using EventStore.ClientAPI;

namespace EventStore.EventStoreDb.Connection
{
    public class EventStoreConnectionWrapper : IEventStoreConnectionWrapper
    {
        public IEventStoreConnection Connection { get; }
        public EventStoreConfig Configuration { get; }

        public EventStoreConnectionWrapper(EventStoreConfig config)
        {
            Configuration = config;
            Connection = EventStoreConnection.Create(Configuration.ConnectionString);
            Connection.ConnectAsync().GetAwaiter().GetResult();
        }
    }
}
