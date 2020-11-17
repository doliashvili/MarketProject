using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;

namespace Market.Domain.Products.Commands
{
    public class DeleteProductCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }

        public DeleteProductCommand(Guid id,CommandMeta commandMeta,
            long? exceptionVersion = null) : base(commandMeta,exceptionVersion)
        {
            Id = id;
        }

    }
}
