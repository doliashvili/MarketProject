using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Commands;
using Core.Commands.Implementation;
using Core.InternalEventSystem;
using Core.Queries;
using Core.Queries.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Register InternalEventPublisher as a transient service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInternalEventPublisher(this IServiceCollection services)
        {
            services.AddTransient<IInternalEventPublisher, InternalEventPublisher>();
            return services;
        }

        
        /// <summary>
        /// Register CommandSender as a transient service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommandSender(this IServiceCollection services)
        {
            services.AddTransient<ICommandSender, CommandSender>();
            return services;
        }
        
        /// <summary>
        /// Registers command handlers as a transient services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (!assemblies.Any())
                throw new ArgumentNullException(nameof(assemblies));

            var handlerInterfaceType = typeof(ICommandHandler<>);

            //Find handlers and generic parameters
            var handlers
                = FindHandlersByGenericInterface(handlerInterfaceType, assemblies);
            
            ThrowIfDuplicates(handlers, handlerInterfaceType);
            
            //Register as transient service
            foreach (var handler in handlers)
            {
                var genericInterfaceType = handler.GetInterfaces()
                    .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == handlerInterfaceType);

                services.AddTransient(genericInterfaceType, handler);
            }

            return services;
        }
        
        
        /// <summary>
        /// Registers event handlers as a transient services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddInternalEventHandlers(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (!assemblies.Any())
                throw new ArgumentNullException(nameof(assemblies));

            var handlerInterfaceType = typeof(IInternalEventHandler<>);

            //Find handlers and generic parameters
            var handlers
                = FindHandlersByGenericInterface(handlerInterfaceType, assemblies).ToList();
            
            //Register as transient service
            foreach (var handler in handlers)
            {
                var genericInterfaceType = handler.GetInterfaces()
                    .First(x=>x.IsGenericType && x.GetGenericTypeDefinition() == handlerInterfaceType);

                services.AddTransient(genericInterfaceType, handler);
            }

            return services;
        }

        
        /// <summary>
        /// Registers query handlers as a transient services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddQueryHandlers(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (!assemblies.Any())
                throw new ArgumentNullException(nameof(assemblies));

            var registeredServices = new List<Type>();
            var genericInterfaceType = typeof(IQueryHandler<,>);

            foreach (var assembly in assemblies)
            {
                var handlerTypes = assembly.GetTypes()
                    .Where(x => x.IsClass && !x.IsAbstract
                                          && x.GetInterfaces()
                                              .Any(i => i.IsGenericType
                                                        && i.GetGenericTypeDefinition() == genericInterfaceType))
                    .ToList();

                foreach (var handlerType in handlerTypes)
                {
                    var genericInterface = handlerType.GetInterfaces()
                        .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
                    if(registeredServices.Any(x=>x == genericInterface))
                        throw new InvalidOperationException($"Duplicate QueryHandler {genericInterface.Name}");
                    registeredServices.Add(genericInterface);
                    services.AddTransient(genericInterface, handlerType);
                }
            }
            
            return services;
        }

        /// <summary>
        /// Register QueryProcessor as a transient service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueryProcessor(this IServiceCollection services)
        {
            services.AddTransient<IQueryProcessor, QueryProcessor>();
            return services;
        }

        /// <summary>
        /// Registers Cqrs objects(QueryHandlers, QueryProcessor, CommandSender, CommandHandler) as a transient services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddQueryHandlers(assemblies)
                .AddQueryProcessor()
                .AddCommandSender()
                .AddCommandHandlers(assemblies);

            return services;
        }


        /// <summary>
        /// Register InternalEventPublisher and InternalEventHandler classes as a transient services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddInternalEventingSystem(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddInternalEventPublisher()
                .AddInternalEventHandlers(assemblies);

            return services;
        }



        #region Private methods


        // Checks duplicates and throws if found
        private static void ThrowIfDuplicates(List<Type> handlers, Type genericInterfaceType)
        {
            if(null == handlers) return;
            var genericInterfaces = handlers.SelectMany(x => x.GetInterfaces()
                .Where(i => i == genericInterfaceType)).ToList();

            var duplicates = genericInterfaces.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x=>x.Key.FullName).Distinct().ToArray();
            if (!duplicates.Any()) return;
            var joinString = string.Join(',', duplicates);
            throw new InvalidOperationException($"Duplicate handlers detected for types: {joinString}");
        }
        
        
        
        // Scan assemblies and find handler types
        private static List<Type> FindHandlersByGenericInterface
            (Type genericInterfaceType, params Assembly[] assemblies)
        {
            // Concrete classes                 
            var typeList = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var handlers = assembly.GetTypes()
                    .Where(t => t.IsClass
                                && !t.IsAbstract
                                && t.GetInterfaces()
                                    .Any(x=>x.IsGenericType 
                                            && x.GetGenericTypeDefinition() == genericInterfaceType))
                                    .ToList();

                typeList.AddRange(handlers);
            }

            return typeList;
        }
        
        #endregion
    }
}