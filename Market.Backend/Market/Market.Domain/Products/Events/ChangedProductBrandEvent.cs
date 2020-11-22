using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductBrandEvent : DomainEvent<Product, Guid>
    {
        public string Brand { get; set; }

        public ChangedProductBrandEvent(string brand, Product product, ICommand command)
        : base(product, command)
        {
            Brand = brand;
        }

        public ChangedProductBrandEvent()
        {

        }
    }
}
