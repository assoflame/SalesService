namespace SalesService.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public byte[] Avatar { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }       

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<UserRating> RatingsAsCustomer { get; set; }
        public IEnumerable<UserRating> RatingsAsSeller { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }
        public IEnumerable<Chat> ChatsAsCustomer { get; set; }
        public IEnumerable<Chat> ChatsAsSeller { get; set; }
    }
}
