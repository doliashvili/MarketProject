using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductForBabyCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public bool? ForBaby { get; private set; }

        [JsonConstructor]
        public ChangeProductForBabyCommand(Guid id, bool? forBaby, CommandMeta commandMeta, long? expectedVersion)
        : base(commandMeta, expectedVersion)
        {
            Id = id;
            ForBaby = forBaby;
        }

        public ChangeProductForBabyCommand()
        {

        }
    }
}
