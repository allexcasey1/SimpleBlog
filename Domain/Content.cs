namespace Domain
{
    public class Content
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}