﻿using System;
using System.Collections.Generic;
using Market.Domain.Products.Enums;
using Market.Domain.Products.ValueObjects;

namespace Market.Domain.Products.Entities
{
    public partial class Product
    {
        public decimal Price { get; private set; }
        public bool IsDeleted { get; private set; }
        public string Color { get; private set; }
        public string Brand { get; private set; }
        public string ProductType { get; set; }
        public Weight Weight { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Gender Gender { get; private set; }
        public bool? ForBaby { get; private set; }
        public string Size { get; private set; }
        public float Discount { get; private set; }
        public decimal DiscountPrice => (Price - (Price * (decimal)Discount));
        public DateTime CreateTime { get; private set; }
        public DateTime Expiration { get; private set; }
        public List<Image> Images { get; private set; }
    }
}
