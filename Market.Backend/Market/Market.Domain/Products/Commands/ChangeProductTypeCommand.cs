using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductTypeCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string ProductType { get; private set; }

        [JsonConstructor]
        public ChangeProductTypeCommand(Guid id, string productType,CommandMeta commandMeta,long? expectedVersion)
        : base(commandMeta,expectedVersion)
        {
            Id = id;
            ProductType = productType;
        }

        public ChangeProductTypeCommand()
        {
            
        }
    }
}
