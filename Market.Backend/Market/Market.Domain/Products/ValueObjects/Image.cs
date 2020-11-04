using Core.Domain;

namespace Market.Domain.Products.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public string ImageUrl { get; set; }
        public bool IsMainImage { get; set; }
    }
}
