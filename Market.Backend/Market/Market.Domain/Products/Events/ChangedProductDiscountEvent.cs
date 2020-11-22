using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductDiscountEvent : DomainEvent<Product, Guid>
    {
        public float Discount { get; set; }

        public ChangedProductDiscountEvent(float discount, Product product, ICommand command)
        : base(product, command)
        {
            Discount = discount;
        }
        public ChangedProductDiscountEvent()
        {

        }
    }
}
