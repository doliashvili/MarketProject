using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;

namespace Core.Tests.CarApp.Commands
{
    public class CreateCar : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public string Model { get; protected set; }
        public int HorsePower { get; protected set; }

        public CreateCar(Guid id, string model, int horsePower, CommandMeta commandMeta)
            : base(commandMeta)
        {
            Id = id;
            Model = model;
            HorsePower = horsePower;
        }
    }
}
