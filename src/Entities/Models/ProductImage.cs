namespace SalesService.Entities.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Data { get; set; }

        public Product Product { get; set; }
    }
}
