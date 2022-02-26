namespace Application.Models
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid? BlogId { get; set; } 
        public ContentDto Content { get; set; } = new ContentDto();
        
    }

    public class ContentDto 
    {
        public Guid Id { get; set; }
        public Guid? BlogPostId { get; set; }
        public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
        
    }
    public class SectionDto
    {
        public Guid Id { get; set; }
        public Guid? ContentId { get; set; }
        public string SectionHeader { get; set; } = string.Empty;
        public string SectionText { get; set; } = string.Empty;
    }
}