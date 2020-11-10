using System;
using Core.Repository;
using Core.Tests.CarApp.DomainObjects;
using EventStore.MSSQL;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Tests
{
    public static class DependencyInjection
    {
        private static IServiceCollection _services = null;
        private static IServiceProvider _provider = null;

        public static T GetService<T>()
        {
            if (_services == null)
            {
                _services = new ServiceCollection();
                RegisterServices();
                _provider = _services.BuildServiceProvider();
            }

            return _provider.GetRequiredService<T>();
        }


        private static void RegisterServices()
        {
            _services.AddCQRS(typeof(Core.DependencyInjection).Assembly);
            _services.AddInternalEventingSystem(typeof(Core.DependencyInjection).Assembly);
            _services.AddEntityDbEventStore("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;");
        }

    }
}
