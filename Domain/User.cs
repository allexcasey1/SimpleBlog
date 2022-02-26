using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    { 
        public string DisplayName { get; set; } = string.Empty;
        public Blog? Blog { get; set; }
    }
}