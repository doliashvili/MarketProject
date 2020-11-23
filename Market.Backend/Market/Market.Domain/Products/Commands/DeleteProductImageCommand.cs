using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class DeleteProductImageCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string PublicId { get; private set; }

        [JsonConstructor]
        public DeleteProductImageCommand(Guid id, string publicId, CommandMeta commandMeta, long? expectedVersion)
        : base(commandMeta, expectedVersion)
        {
            Id = id;
            PublicId = publicId;
        }

        public DeleteProductImageCommand()
        {

        }
    }
}
