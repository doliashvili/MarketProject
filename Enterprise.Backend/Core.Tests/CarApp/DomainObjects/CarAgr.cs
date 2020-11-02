using Core.Tests.CarApp.Commands;
using Core.Tests.CarApp.Events;

namespace Core.Tests.CarApp.DomainObjects
{
    public partial class CarAgr
    {
        public CarAgr() { }

        public CarAgr(CreateCar command) : base(command.Id)
        {
            ApplyChange(
                new CarCreated(
                    command.Model, 
                    command.HorsePower, 
                    this, 
                    command));
        }

        public void Apply(CarCreated e)
        {
            if(Id == default)
                Id = e.AggregateId;
            Model = e.Model;
            HorsePower = e.HorsePower;
        }

    }
}
