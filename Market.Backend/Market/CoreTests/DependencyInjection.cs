using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Core;
using EventStore.EventStoreDb;
using ReadModelRepository.RavenDb;

namespace CoreTests
{
    public static class DependencyInjection
    {
        
        private static IServiceCollection _services;

        public static IServiceProvider GetServiceProvider()
        {
            _services ??= new ServiceCollection();
            //todo add registration
            return _services.BuildServiceProvider();
        }

        private static void RegisterServices()
        {
            _services.AddCQRS(typeof(DependencyInjection).Assembly);
            _services.AddInternalEventingSystem(typeof(DependencyInjection).Assembly);
            _services.AddRavenDbReadModels(new RavenSettings()
                {Urls = new[] {"http://127.0.0.1:8080"}, Database = "ReadModels"});

        }


    }
}
