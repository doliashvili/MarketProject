﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;
using Market.ReadModels.Models;

namespace Market.ReadModels.Read.Products.Queries
{
    public class GetProducts : IQuery<IReadOnlyList<ProductReadModel>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
