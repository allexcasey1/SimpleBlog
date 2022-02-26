namespace Domain    
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Meta { get; set; }        
        public Guid BlogId { get; set; }
        public Blog? Blog { get; set; }

        public Content Content { get; set; } = new Content();
        public ICollection<Comment>? Comments { get; set; } = new HashSet<Comment>();
    }
}