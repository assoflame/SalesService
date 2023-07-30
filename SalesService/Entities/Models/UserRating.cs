namespace SalesService.Entities.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public int StarsCount { get; set; }

        public User Customer { get; set; }
        public User Seller { get; set; }
    }
}
