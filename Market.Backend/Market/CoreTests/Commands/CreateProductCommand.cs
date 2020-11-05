using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;

namespace CoreTests.Commands
{
    public class CreateProductCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string Name { get; private set; }

        public CreateProductCommand(string name,CommandMeta commandMeta)
        : base(commandMeta)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

    }
}
