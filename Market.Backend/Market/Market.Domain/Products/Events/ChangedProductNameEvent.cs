using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductNameEvent : DomainEvent<Product,Guid>
    {
        public string Name { get; private set; }

        public ChangedProductNameEvent(string name,Product product,ICommand command) : base(product,command)
        {
            Name = name;
        }

        public ChangedProductNameEvent()
        {
            
        }
    }
}
