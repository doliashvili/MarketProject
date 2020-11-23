using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductForBabyEvent : DomainEvent<Product, Guid>
    {
        public bool? ForBaby { get; set; }

        public ChangedProductForBabyEvent(bool? forBaby, Product product, ICommand command)
        : base(product, command)
        {
            ForBaby = forBaby;
        }
        public ChangedProductForBabyEvent()
        {

        }
    }
}
