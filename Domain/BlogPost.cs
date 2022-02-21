namespace Domain    
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Meta { get; set; }
        
        public Content Content { get; set; } = new Content();
    }
}