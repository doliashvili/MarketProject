using System;
using System.Collections.Generic;
using Core.Commands;
using Core.Events.DomainEvents;
using Market.Domain.Products.Entities;
using Market.Domain.Products.Enums;
using Market.Domain.Products.ValueObjects;

namespace Market.Domain.Products.Events
{
    public class CreatedProductEvent : DomainEvent<Product, Guid>
    {
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public string Brand { get; private set; }
        public string ProductType { get; set; }
        public Weight Weight { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Gender Gender { get; private set; }
        public bool ForBaby { get; private set; }
        public string Size { get; private set; }
        public float Discount { get; private set; }
        public DateTime CreateTime { get; private set; }
        public List<Image> Images { get; private set; }
        public DateTime Expiration { get; private set; }

        public CreatedProductEvent() { }

        public CreatedProductEvent(decimal price,
            string color,
            string brand,
            string productType,
            Weight weight,
            string name,
            string description,
            Gender gender,
            bool forBaby,
            string size, 
            float discount,
            DateTime createTime,
            List<Image> images,
            DateTime expiration,
            Product aggregate,
            ICommand command ) : base(aggregate,command)
        {
            Price = price;
            Color = color;
            Brand = brand;
            ProductType = productType;
            Weight = weight;
            Name = name;
            Description = description;
            Gender = gender;
            ForBaby = forBaby;
            Size = size;
            Discount = discount;
            CreateTime = createTime;
            Images = images;
            Expiration = expiration;
        }
    }
}
