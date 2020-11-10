using Core.Domain;
using System;
using CoreTests.Commands;
using CoreTests.Events;


namespace CoreTests.Aggregates
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public ProductAggregate()
        {
            
        }

        public ProductAggregate(CreateProductCommand command) : base(command.Id)
        {
            Name = command.Name;
            Id = command.Id;

            ApplyChange(new CreatedProductEvent(command.Name,this,command));
        }



    }
}
