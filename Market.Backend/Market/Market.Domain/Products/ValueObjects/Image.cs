using Core.Domain;

namespace Market.Domain.Products.DomainObjects.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public string ImageUrl { get; set; }
        public bool IsMainImage { get; set; }
    }
}
