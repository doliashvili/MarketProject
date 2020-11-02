namespace EventStore.EventStoreDb
{
    public class EventStoreConfig
    {
        public string ConnectionString { get; set; }
        public int BatchSize { get; set; }
    }
}
