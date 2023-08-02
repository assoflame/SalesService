namespace SalesService.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }

        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
