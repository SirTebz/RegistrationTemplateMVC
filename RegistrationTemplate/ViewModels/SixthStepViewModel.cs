using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.ViewModels
{
    public class SixthStepViewModel
    {
        [Required(ErrorMessage = "Verification code is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid 6-digit code.")]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; } = null!;
    }
}
