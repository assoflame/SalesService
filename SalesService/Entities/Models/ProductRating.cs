namespace SalesService.Entities.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int StarsCount { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
