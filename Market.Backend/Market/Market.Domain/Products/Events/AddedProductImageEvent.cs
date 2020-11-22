using System;
using System.Collections.Generic;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;
using Market.Domain.Products.ValueObjects;

namespace Market.Domain.Products.Events
{
    public class AddedProductImageEvent : DomainEvent<Product, Guid>
    {
        public List<Image> Images { get; private set; }

        public AddedProductImageEvent(List<Image> images, Product product, ICommand command)
        : base(product, command)
        {
            Images = images;
        }

        public AddedProductImageEvent()
        {

        }
    }
}
