using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    { 
        public string DisplayName { get; set; } = string.Empty;
        public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}