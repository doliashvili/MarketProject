using Market.Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Commands;
using Core.Repository;
using Market.Domain.FileManager;
using Market.Domain.Products.Entities;
using Market.Domain.Products.ValueObjects;

namespace Market.Application.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IAggregateRepository<Product, Guid> _repo;
        //todo twice add image ?  here and readmodel dbs?
        private readonly IFileManager _fileManager;

        public ProductCommandHandler(IAggregateRepository<Product, Guid> repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var listImages= new List<Image>();

            foreach (var formFile in command.IFormFiles)
            {
                listImages.Add(
                    new Image()
                        {ImageUrl = await _fileManager.SaveImage(formFile)}
                    );
            }

            command.SetImages(listImages);

            var aggregateProduct = new Product(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }
    }
}
