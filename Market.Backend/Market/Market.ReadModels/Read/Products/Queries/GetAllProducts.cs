using Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Market.ReadModels.Models;

namespace Market.ReadModels.Read.Queries.Products
{
    public class GetAllProducts : IQuery<ProductReadModel>, IQuery<IReadOnlyList<ProductReadModel>>
    {

    }
}
