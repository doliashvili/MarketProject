using Core.Domain;
using Market.Domain.Products.Enums;

namespace Market.Domain.Products.ValueObjects
{
    public class Weight : ValueObject<Weight>
    {
        public decimal Value { get; set; }
        public WeightType WeightType { get; set; }
    }
}
