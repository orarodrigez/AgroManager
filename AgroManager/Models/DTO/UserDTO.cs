using System.ComponentModel.DataAnnotations;

namespace AgroManager.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        [MaxLength(320)]
        public required string Email { get; set; } 
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class AddUserDTO
    {
        public string Username { get; set; }   = string.Empty;
        [EmailAddress]
        [MaxLength(320)]
        public required string Email { get; set; } 
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
    public class UpdateUserDTO
    {
        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        [MaxLength(320)]
        public required string Email { get; set; } 
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
