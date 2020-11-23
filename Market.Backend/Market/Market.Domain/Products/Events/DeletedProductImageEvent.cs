using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;

namespace Market.Domain.Products.Events
{
    public class DeletedProductImageEvent : DomainEvent<Product,Guid>
    {
        public string PublicId { get; set; }

        public DeletedProductImageEvent(string publicId,Product product,ICommand command)
        : base(product,command)
        {
            PublicId = publicId;
        }

        public DeletedProductImageEvent()
        {
            
        }
    }
}
