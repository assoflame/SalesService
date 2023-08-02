namespace SalesService.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IFormFile? Avatar { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<UserRating> Ratings { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }
        public IEnumerable<ProductRating> ProductRatings { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
    }
}
