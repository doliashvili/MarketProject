using System;
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
                _provider = _services.BuildServiceProvider();
            }

            return _provider.GetRequiredService<T>();
        }


        private static void RegisterServices()
        {
            _services.AddCQRS(typeof(DependencyInjection).Assembly);
            _services.AddInternalEventingSystem(typeof(DependencyInjection).Assembly);

        }

    }
}
