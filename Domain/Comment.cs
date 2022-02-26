namespace Domain
{
    public class Comment
    {
        public int Id { get; set;}
        public string Body { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public Guid BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}