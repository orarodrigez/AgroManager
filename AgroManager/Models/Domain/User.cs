using System.ComponentModel.DataAnnotations;

namespace AgroManager.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        [MaxLength(320)]
        public required string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
