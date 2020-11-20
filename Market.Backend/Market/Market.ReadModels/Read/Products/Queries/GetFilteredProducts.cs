using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;
using Market.Domain.Constants;
using Market.Domain.Products.Enums;
using Market.ReadModels.Models;
using Newtonsoft.Json;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetFilteredProducts : IQuery<(IReadOnlyList<ProductReadModel> page,int pageCount)>
    {
        [JsonConstructor]
        public GetFilteredProducts(decimal? priceFrom,
            decimal? priceTo,
            string color,
            string brand,
            string productType,
            string name,
            Gender? gender,
            bool? forBaby,
            string size,
            int? page,
            int? pageSize)
        {
            PriceFrom = priceFrom?? SqlDefaultValues.MinDecimal;
            PriceTo = priceTo?? SqlDefaultValues.MaxDecimal;
            Color = color;
            Brand = brand;
            ProductType = productType;
            Name = name;
            Gender = gender;
            ForBaby = forBaby;
            Size = size;
            Page = page;
            PageSize = pageSize;
        }

        public GetFilteredProducts()
        {
            
        }

        public decimal? PriceFrom { get; set; } = SqlDefaultValues.MinDecimal;
        public decimal? PriceTo { get; set; } = SqlDefaultValues.MaxDecimal;
        public string Color { get; set; }
        public string Brand { get; set; }
        public string ProductType { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public bool? ForBaby { get; set; }
        public string Size { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
