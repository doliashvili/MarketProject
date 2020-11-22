using System;
using System.Collections.Generic;
using System.Text;
using Core.Commands;
using Market.Domain.Products.ValueObjects;
using Newtonsoft.Json;

namespace Market.Domain.Products.Commands
{
    public class AddProductImageCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        public ICollection<string> ImagesString { get; private set; }
        public List<Image> Images { get; private set; } = new List<Image>();

        [JsonConstructor]
        public AddProductImageCommand(Guid id, ICollection<string> imagesString, CommandMeta commandMeta, long? expectedVersion)
        : base(commandMeta, expectedVersion)
        {
            Id = id;
            ImagesString = imagesString;
        }

        public AddProductImageCommand()
        {

        }
    }
}
