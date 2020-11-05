using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;

namespace CoreTests.Commands
{
    public class ChangeProductNameCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string Name { get; private set; }

        public ChangeProductNameCommand(Guid id, string name,CommandMeta commandMeta)
        : base(commandMeta)
        {
            Id = id;
            Name = name;
        }
    }
}
