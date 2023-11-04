namespace SalesService.Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public DateTime CreationDate { get; set; }

        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
