namespace SalesService.Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public DateTime CreationDate { get; set; }

        public User Customer { get; set; }
        public User Seller { get; set; }
    }
}
