using System;

namespace Market.Domain.Products.Exceptions
{
    public class WrongPriceException : Exception
    {
        public override string Message { get; } = "Price must be more than zero";
    }
}
