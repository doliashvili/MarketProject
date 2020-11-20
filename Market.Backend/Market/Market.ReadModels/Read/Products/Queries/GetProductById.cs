using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;
using Market.ReadModels.Models;
using Newtonsoft.Json;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetProductById : IQuery<ProductReadModel>
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public GetProductById(Guid id)
        {
            Id = id;
        }

        public GetProductById()
        {
            
        }
    }
}
