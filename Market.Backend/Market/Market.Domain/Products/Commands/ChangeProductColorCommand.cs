using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductColorCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string Color { get; private set; }

        [JsonConstructor]
        public ChangeProductColorCommand(Guid id, string color, CommandMeta commandMeta, long? expectedVersion)
            : base(commandMeta, expectedVersion)
        {
            Id = id;
            Color = color;
        }

        public ChangeProductColorCommand()
        {
            
        }
    }
}
