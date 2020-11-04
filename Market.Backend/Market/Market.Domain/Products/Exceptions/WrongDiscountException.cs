using System;

namespace Market.Domain.Products.Exceptions
{
    public class WrongDiscountException : Exception
    {
        public override string Message { get; } = "Discount range must be 0.0 : 1.0";
    }
}
