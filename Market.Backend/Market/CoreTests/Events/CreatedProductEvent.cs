using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Core.Events.DomainEvents;
using CoreTests.Aggregates;

namespace CoreTests.Events
{
    public class CreatedProductEvent : DomainEvent<ProductAggregate,Guid>
    {
        public string Name { get; private set; }
        public CreatedProductEvent()
        {
            
        }

        public CreatedProductEvent(string name,ProductAggregate productAggregate,ICommand command)
        : base(productAggregate,command)
        {
            Name = name;
        }
    }
}
