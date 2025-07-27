using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.Models
{
    public class RegistrationSession
    {
        [Key]
        public Guid RegistrationId { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? PasswordHash { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RecoveryEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
