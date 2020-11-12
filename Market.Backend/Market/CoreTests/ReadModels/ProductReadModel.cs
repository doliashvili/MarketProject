using System;
using System.Collections.Generic;
using System.Text;
using Core.Queries;

namespace CoreTests.ReadModels
{
    public class ProductReadModel : IReadModel<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
