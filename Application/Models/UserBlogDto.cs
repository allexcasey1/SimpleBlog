
namespace Application.Models
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public String UserName { get; set; } = string.Empty;
        public BlogDto Blog { get; set; } = new BlogDto();
    }

    public class BlogDto 
    {
        public Guid Id { get; set; }
        public List<BlogPostDto> BlogPosts { get; set; } = new List<BlogPostDto>();
    }
}