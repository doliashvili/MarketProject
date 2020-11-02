using System;
using Core.Commands;
using Core.Events.DomainEvents;
using Core.Tests.CarApp.DomainObjects;

namespace Core.Tests.CarApp.Events
{
    public class CarCreated : DomainEvent<CarAgr, Guid>
    {
        public string Model { get; private set; }
        public int HorsePower { get; private set; }

        public CarCreated(string model, int horsePower, CarAgr agr, ICommand command) : base(agr, command)
        {
            Model = model;
            HorsePower = horsePower;
        }
    }
}
