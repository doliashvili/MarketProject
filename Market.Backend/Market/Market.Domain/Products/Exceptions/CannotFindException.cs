using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Domain.Products.Exceptions
{
    public class CannotFindException : Exception
    {
        public override string Message { get; }

        public CannotFindException(string message)
        {
            Message = message;
        }
    }
}
