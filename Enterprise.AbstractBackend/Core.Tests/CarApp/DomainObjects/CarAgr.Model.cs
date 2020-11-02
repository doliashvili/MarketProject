using System;
using Core.Domain;

namespace Core.Tests.CarApp.DomainObjects
{
    public partial class CarAgr : AggregateRoot<Guid>
    {
        public int HorsePower { get; protected set; }
        public string Model { get; protected set; }
    }
}
