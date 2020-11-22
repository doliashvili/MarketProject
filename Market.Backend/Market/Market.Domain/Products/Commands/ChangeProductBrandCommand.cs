using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductBrandCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string Brand { get; private set; }

        [JsonConstructor]
        public ChangeProductBrandCommand(Guid id, string brand, CommandMeta commandMeta, long? expectedVersion) 
            : base(commandMeta, expectedVersion)
        {
            Id = id;
            Brand = brand;
        }

        public ChangeProductBrandCommand()
        {
            
        }
    }
}
