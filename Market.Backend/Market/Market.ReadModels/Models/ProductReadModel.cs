using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;
using Market.Domain.Products.Enums;
using Market.Domain.Products.ValueObjects;

namespace Market.ReadModels.Models
{
    public class ProductReadModel : IReadModel<Guid>
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }
        public Weight Weight { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string Size { get; set; }
        public float Discount { get; set; }
        public decimal DiscountPrice => (Price - (Price * (decimal)Discount));
        public DateTime CreateTime { get; set; }
        public DateTime Expiration { get; set; }
        public List<Image> Images { get; set; }
    }
}
