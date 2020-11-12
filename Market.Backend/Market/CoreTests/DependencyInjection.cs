using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Core.Models;
using Core.Repository;
using EventStore.MSSQL;
using Repository.MSSQL.Concrete;


namespace CoreTests
{
    public static class DependencyInjection
    {
        
        private static IServiceCollection _services;

        public static IServiceProvider GetServiceProvider()
        {
            _services ??= new ServiceCollection();
            RegisterServices();
            return _services.BuildServiceProvider();
        }

        private static void RegisterServices()
        {
            _services.AddCQRS(typeof(DependencyInjection).Assembly);
            _services.AddInternalEventingSystem(typeof(DependencyInjection).Assembly);
            _services.AddEntityDbEventStore(
                "Server=localhost;Initial Catalog=EventStore;Persist Security Info=False;Integrated Security=true;");
            
                
            //_services.AddRavenDbReadModels(new RavenSettings()
            //    {Urls = new[] {"http://127.0.0.1:8080"}, Database = "ReadModels"});
            //_services.AddMongoDbEventStore(
            //    new MongoConfig()
            //    {
            //       ConnectionString = "mongodb+srv://avtandilm:e5115135124w@cluster0.fcryz.azure.mongodb.net/BookDB?retryWrites=true&w=majority"
            //    }, new EventStoreConfig() {BatchSize = 200});

            //_services.AddEventStore(new EventStoreConfig(){BatchSize = 200, ConnectionString ="https://www.eventstore.com/event-store-cloud" })
        }


    }
}
