
namespace Api.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; } = DateTime.Now.AddDays(7);
        public string Username { get; set; } = string.Empty;
    }
}