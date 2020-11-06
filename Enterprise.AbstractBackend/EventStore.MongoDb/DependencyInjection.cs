using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
using Core.Models;
using Core.Repository;
using EventStore.MongoDb.Abstract;
using EventStore.MongoDb.Concrete;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace EventStore.MongoDb
{
    public static class DependencyInjection
    {
        internal class CustomSerializationProvider : IBsonSerializationProvider
        {
            private static readonly IBsonSerializer<decimal> DecimalSerializer =
                new DecimalSerializer(BsonType.Decimal128);

            private static readonly IBsonSerializer NullableSerializer =
                new NullableSerializer<decimal>(DecimalSerializer);

            private static readonly IBsonSerializer GuidSerializer = new GuidSerializer(GuidRepresentation.Standard);

            public IBsonSerializer GetSerializer(Type type)
            {
                if (type == typeof(decimal)) return DecimalSerializer;
                if (type == typeof(decimal?)) return NullableSerializer;
                if (type == typeof(Guid)) return GuidSerializer;

                return null;
            }
        }


        /// <summary>
        /// Add mongoDb implementation of IEventStore
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mongoConfig"></param>
        /// <param name="eventStoreConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDbEventStore(this IServiceCollection services,
            MongoConfig mongoConfig, EventStoreConfig eventStoreConfig)
        {
            BsonSerializer.RegisterSerializationProvider(new CustomSerializationProvider());
            services.AddSingleton(mongoConfig)
                    .AddSingleton(eventStoreConfig)
                    .AddSingleton<IMongoConnectionWrapper, MongoConnectionWrapper>()
                    .RegisterStores(false);
            return services;
        }


        public static IServiceCollection RegisterStores(this IServiceCollection services,
            bool excludeTests = true)
        {
            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => !x.IsDynamic).ToArray();

            if (excludeTests)
                assemblies = assemblies
                    .Where(x =>
                        // ReSharper disable once PossibleNullReferenceException
                        !x.FullName.Contains("Test", StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

            var aggregates = AssemblyScanner.FindAggregates(assemblies);
            foreach (var kv in aggregates)
            {
                var eventStoreInterface = typeof(IEventStore<,>).MakeGenericType(kv.Key, kv.Value);
                var concreteEventStore = typeof(MongoEventStore<,>).MakeGenericType(kv.Key, kv.Value);
                services.AddTransient(eventStoreInterface, concreteEventStore);
            }

            return services;
        }

    }
}
