using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductPriceCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public decimal Price { get; private set; }

        [JsonConstructor]
        public ChangeProductPriceCommand(Guid id, decimal price,CommandMeta commandMeta,long? expectedVersion=null) 
            : base(commandMeta,expectedVersion)
        {
            Id = id;
            Price = price;
        }

        public ChangeProductPriceCommand()
        {
            
        }
    }
}
