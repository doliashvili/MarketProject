using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductTypeEvent : DomainEvent<Product, Guid>
    {
        public string ProductType { get; set; }

        public ChangedProductTypeEvent(string productType, Product product, ICommand command)
        : base(product, command)
        {
            ProductType = productType;
        }
        public ChangedProductTypeEvent()
        {

        }
    }
}
