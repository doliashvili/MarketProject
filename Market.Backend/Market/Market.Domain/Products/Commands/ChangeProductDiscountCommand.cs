using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
   public class ChangeProductDiscountCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public float Discount { get; private set; }

        [JsonConstructor]
        public ChangeProductDiscountCommand(Guid id, float discount, CommandMeta commandMeta, long? expectedVersion)
            : base(commandMeta, expectedVersion)
        {
            Id = id;
            Discount = discount;
        }

        public ChangeProductDiscountCommand()
        {
            
        }
    }
}
