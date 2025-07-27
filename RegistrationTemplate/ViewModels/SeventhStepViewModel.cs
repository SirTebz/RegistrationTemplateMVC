using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.ViewModels
{
    public class SeventhStepViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter a valid recovery email address.")]
        [Display(Name = "Recovery Email (Optional)")]
        public string? RecoveryEmail { get; set; }
    }
}
