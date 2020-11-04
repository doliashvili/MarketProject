using System.Collections.Generic;
using Core.Queries;
using Market.ReadModels.Models;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetAllProducts : IQuery<ProductReadModel>, IQuery<IReadOnlyList<ProductReadModel>>
    {

    }
}
