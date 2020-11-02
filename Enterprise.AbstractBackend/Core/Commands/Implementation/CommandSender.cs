using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands.Implementation
{
    public class CommandSender : ICommandSender
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        /// <summary>
        /// Sends command to handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) 
            where T : class, ICommand
        {
            //Get implemented commandHandler for T
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<T>>();

            //return uncompleted Task
            return handler.HandleAsync(command, cancellationToken);
        }
    }
}