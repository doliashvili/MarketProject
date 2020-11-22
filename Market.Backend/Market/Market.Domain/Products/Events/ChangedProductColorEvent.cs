using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class ChangedProductColorEvent : DomainEvent<Product,Guid>
    {
        public string Color { get; set; }

        public ChangedProductColorEvent(string color,Product product,ICommand command) 
        : base(product,command)
        {
            Color = color;
        }

        public ChangedProductColorEvent()
        {
            
        }
    }
}
