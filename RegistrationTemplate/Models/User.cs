using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.Models
{
    [Index(nameof(Email), Name = "UX_Users_Email_Unque", IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string CountryCode { get; set; } = null!;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [EmailAddress]
        public string? RecoveryEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }
    }
}
