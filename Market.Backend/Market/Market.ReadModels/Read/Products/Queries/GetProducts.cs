using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Queries;
using Market.ReadModels.Models;
using Newtonsoft.Json;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetProducts : IQuery<(IReadOnlyList<ProductReadModel> page,int pageCount)>
    {
        [JsonConstructor]
        public GetProducts(int? page, int? pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public GetProducts()
        {
            
        }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
