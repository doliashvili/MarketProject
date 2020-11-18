using System;
using Core.Domain;
using Market.Domain.Products.Commands;
using Market.Domain.Products.Events;
using Market.Domain.Products.Exceptions;

namespace Market.Domain.Products.Entities
{
    public partial class Product : AggregateRoot<Guid>
    {

        public Product() { }

        public Product(CreateProductCommand command) : base(command.Id)
        {
            ValidatePrice(command.Price);
            ValidateDiscount(command.Discount);
            Id = command.Id;
            Price = command.Price;
            Color = command.Color;
            Brand = command.Brand;
            ProductType = command.ProductType;
            Weight = command.Weight;
            Name = command.Name;
            Description = command.Description;
            Gender = command.Gender;
            ForBaby = command.ForBaby;
            Size = command.Size;
            Discount = command.Discount;
            CreateTime = command.CreateTime;
            Expiration = command.Expiration;
            Images = command.Images;
            ApplyChange(new CreatedProductEvent(Price,
                Color,
                Brand,
                ProductType,
                Weight,
                Name,
                Description,
                Gender,
                ForBaby,
                Size,
                Discount,
                CreateTime,
                Images,
                Expiration,
                this,
                command
            ));
        }

        public void Apply(CreatedProductEvent e)
        {
            Id = e.AggregateId;
            Price = e.Price;
            Color = e.Color;
            Brand = e.Brand;
            ProductType = e.ProductType;
            Weight = e.Weight;
            Name = e.Name;
            Description = e.Description;
            Gender = e.Gender;
            ForBaby = e.ForBaby;
            Size = e.Size;
            Discount = e.Discount;
            CreateTime = e.CreateTime;
            Expiration = e.Expiration;
            Images = e.Images;
        }

        public void Delete(DeleteProductCommand command)
        {
            ApplyChange(new DeletedProductEvent(this,command));
        }

        public void Apply(DeletedProductEvent e)
        {
            IsDeleted = true;
        }

        public void ChangeProductName(ChangeProductNameCommand command)
        {
            ApplyChange(new ChangedProductNameEvent(command.Name,this,command));
        }

        public void Apply(ChangedProductNameEvent e)
        {
            Name = e.Name;
        }

        public void ChangeProductPrice(ChangeProductPriceCommand command)
        {
            ValidatePrice(command.Price);
            ApplyChange(new ChangedProductPriceEvent(command.Price,this,command));
        }

        public void Apply(ChangedProductPriceEvent e)
        {
            Price = e.Price;
        }

        #region ValidationMethods
        private void ValidateDiscount(float discount)
        {
            if (discount > 1 || discount < 0)
            {
                throw new WrongDiscountException();
            }
        }

        private void ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new WrongPriceException();
        }
        #endregion
    }
}
