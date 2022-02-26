
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } 
        [Required]
        public string? Username { get; set; } 
        [Required]
        public string? DisplayName { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&]).{9,18}$", ErrorMessage = "Invalid password")]
        public string? Password { get; set; } 
    }
}