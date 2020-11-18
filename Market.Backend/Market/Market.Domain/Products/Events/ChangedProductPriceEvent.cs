using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductPriceEvent : DomainEvent<Product, Guid>
    {
        public decimal Price { get; private set; }

        public ChangedProductPriceEvent(decimal price,Product product,ICommand command) : base(product,command)
        {
            Price = price;
        }

        public ChangedProductPriceEvent()
        {
            
        }
    }
}
