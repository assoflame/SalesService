namespace SalesService.Entities.Models
{
    public class Review
    {
        public int UserWhoRatedId { get; set; }
        public int UserId { get; set; }
        public int StarsCount { get; set; }
        public string? Comment { get; set; }
        public DateTime CreationDate { get; set; }

        public User UserWhoRated { get; set; }
        public User User { get; set; }
    }
}
