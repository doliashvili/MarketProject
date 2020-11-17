using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class DeletedProductEvent : DomainEvent<Product, Guid>
    {
        public DeletedProductEvent()
        {

        }

        public DeletedProductEvent(Product product, ICommand command) : base(product, command)
        {
            
        }
    }
}
