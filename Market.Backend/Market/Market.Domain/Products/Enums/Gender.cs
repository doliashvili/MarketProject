using System;

namespace Market.Domain.Products.Enums
{
    [Flags]
    public enum Gender : byte
    {
        Male = 1,
        Female = 2,
        Other = 3,
    }
}
