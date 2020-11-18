using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class DeleteProductCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }

        [JsonConstructor]
        public DeleteProductCommand(Guid id,CommandMeta commandMeta,
            long? exceptionVersion = null) : base(commandMeta,exceptionVersion)
        {
            Id = id;
        }

        public DeleteProductCommand()
        {
            
        }
    }
}
