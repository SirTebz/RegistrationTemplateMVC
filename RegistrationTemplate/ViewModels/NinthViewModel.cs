using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.ViewModels
{
    public class NinthViewModel
    {
        [Required(ErrorMessage = "You must agree to the terms and privacy policy to proceed.")]
        [Display(Name = "I agree to the Terms and Privacy Policy")]
        public bool Agree { get; set; }
    }
}
