using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;
using Market.ReadModels.Models;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetProductById : IQuery<ProductReadModel>
    {
        public Guid Id { get; set; }
    }
}
