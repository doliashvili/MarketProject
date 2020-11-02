using System;
using System.Collections.Concurrent;

namespace EventStore.EventStoreDb.Helpers
{
    public static class EventTypeInMemoryStore
    {
        private static readonly ConcurrentDictionary<string, Type> EventTypes = new ConcurrentDictionary<string, Type>();

        public static Type GetOrAdd(string assemblyQualifiedName)
        {
            return EventTypes.GetOrAdd(assemblyQualifiedName, v => Type.GetType(assemblyQualifiedName));
        }
    }
}
