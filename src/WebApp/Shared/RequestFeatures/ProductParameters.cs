namespace Shared.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public ProductParameters() => OrderBy = "price";
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; } = decimal.MaxValue;

        public bool ValidPriceRange => MinPrice <= MaxPrice;

    }
}
