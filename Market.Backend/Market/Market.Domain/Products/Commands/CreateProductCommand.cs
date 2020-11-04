﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Commands;
using Market.Domain.Products.Enums;
using Market.Domain.Products.ValueObjects;

namespace Market.Domain.Products.Commands
{
    public class CreateProductCommand : Command<Guid>
    {
        public override Guid Id { get; protected set; }
        [Range(0,int.MaxValue)]
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

        public CreateProductCommand(long id,
            decimal price,
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
            DateTime expiration,
            List<Image> images,
            CommandMeta commandMeta,
            long? exceptionVersion=null) : base(commandMeta,exceptionVersion)
        {
            Id = Guid.NewGuid();
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
