using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.MessageBroker.Helpers
{
    public static class ConsumerFinder
    {
        private static List<Type> _consumers = default;
        public static volatile bool IsScanned = false;
        private static readonly object LockObj = new object();


        public static IReadOnlyList<Type> GetConsumers()
        {
            lock (LockObj)
            {
                return !IsScanned ? default(IReadOnlyList<Type>) : _consumers;
            }
        }

        public static IReadOnlyList<Type> FindAll(params Assembly[] assemblies)
        {
            lock (LockObj)
            {
                if (!assemblies.Any())
                    throw new InvalidOperationException($"parameter: {nameof(assemblies)} is empty");

                if (IsScanned) return _consumers;

                _consumers = new List<Type>();
                assemblies.ToList().ForEach(assembly =>
                {
                    var types = assembly.GetTypes()
                        .Where(x => x.IsClass
                                    && !x.IsAbstract
                                    && typeof(IMessageConsumer).IsAssignableFrom(x)
                                    && x.GetCustomAttribute<MessageHandlerAttribute>() != null).ToList();
                    if (types.Any())
                        _consumers.AddRange(types);
                });
                IsScanned = true;
                return _consumers;
            }
        }
    }
}
