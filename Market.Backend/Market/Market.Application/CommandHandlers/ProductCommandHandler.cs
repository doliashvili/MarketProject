using Market.Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Core.Commands;
using Core.Repository;
using Market.Application.Cloudinary;
using Market.Domain.Products.Entities;
using Market.Domain.Products.Events;
using Market.Domain.Products.Exceptions;
using Market.Domain.Products.ValueObjects;
using PhotoSauce.MagicScaler;

namespace Market.Application.CommandHandlers
{
    public class ProductCommandHandler :
        ICommandHandler<CreateProductCommand>,
        ICommandHandler<DeleteProductCommand>,
        ICommandHandler<ChangeProductNameCommand>,
        ICommandHandler<ChangeProductPriceCommand>,
        ICommandHandler<ChangeProductBrandCommand>,
        ICommandHandler<ChangeProductColorCommand>,
        ICommandHandler<ChangeProductTypeCommand>,
        ICommandHandler<ChangeProductDiscountCommand>,
        ICommandHandler<AddProductImageCommand>
    {
        private readonly IAggregateRepository<Product, Guid> _repo;

        public ProductCommandHandler(IAggregateRepository<Product, Guid> repo)
        {
            _repo = repo;
        }
        public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var imageUploader= new CloudinaryImageUploader();

            foreach (var imageString in command.ImagesString)
            {
                var bytesImg = Convert.FromBase64String(imageString.Substring(imageString.IndexOf(',') + 1));
                var img = await imageUploader.Upload(Guid.NewGuid().ToString(), bytesImg, 500, 450);
                command.Images.Add(new Image(){ImageUrl = img.SecureUrl.AbsoluteUri});
            }

            if (command.Images.Count > 0)
            {
                command.Images[0].IsMainImage = true;
            }

            var aggregateProduct = new Product(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.Delete(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductNameCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductName(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductPriceCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }
         
            aggregateProduct.ChangeProductPrice(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductBrandCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken:cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductBrand(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductColorCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductColor(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductTypeCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductType(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(ChangeProductDiscountCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            aggregateProduct.ChangeProductDiscount(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }

        public async Task HandleAsync(AddProductImageCommand command, CancellationToken cancellationToken = default)
        {
            var aggregateProduct = await _repo.GetAsync(command.Id, cancellationToken: cancellationToken);
            if (aggregateProduct == null)
            {
                throw new CannotFindException("No data on this Id");
            }

            var imageUploader = new CloudinaryImageUploader();

            foreach (var imageString in command.ImagesString)
            {
                var bytesImg = Convert.FromBase64String(imageString.Substring(imageString.IndexOf(',') + 1));
                var img = await imageUploader.Upload(Guid.NewGuid().ToString(), bytesImg, 500, 450);
                command.Images.Add(new Image() { ImageUrl = img.SecureUrl.AbsoluteUri });
            }


            aggregateProduct.AddProductImage(command);
            await _repo.SaveAsync(aggregateProduct, cancellationToken);
        }
    }
}
