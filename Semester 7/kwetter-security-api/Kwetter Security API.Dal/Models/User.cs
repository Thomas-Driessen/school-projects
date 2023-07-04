namespace Kwetter_Security_API.Dal.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Biography { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Disabled { get; set; }
    }
}