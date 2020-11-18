using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class ChangeProductNameCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        [Required]
        public string Name { get; private set; }

        [JsonConstructor]
        public ChangeProductNameCommand(Guid id,string name,CommandMeta commandMeta,long? expectedVersion=null)
            : base(commandMeta,expectedVersion)
        {
            Id = id;
            Name = name;
        }

        public ChangeProductNameCommand()
        {
            
        }
    }
}
