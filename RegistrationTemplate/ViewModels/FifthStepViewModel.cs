using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace RegistrationTemplate.ViewModels
{
    public class FifthStepViewModel
    {
        [Required(ErrorMessage = "Country code is required.")]
        [Display(Name = "Country")]
        public string CountryCode { get; set; } = null!;
        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
        // List of country codes for the dropdown (populated in controller or view)
        public IEnumerable<SelectListItem>? CountryCodes { get; set; }
        // Custom validation for phone number based on country code using libphonenumber-csharp
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(CountryCode))
            {
                results.Add(new ValidationResult("Country code must be selected.", new[] { nameof(CountryCode) }));
                return results;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                results.Add(new ValidationResult("Phone number is required.", new[] { nameof(PhoneNumber) }));
                return results;
            }
            var phoneUtil = PhoneNumberUtil.GetInstance();
            // Remove whitespace for safety
            var nationalNumber = PhoneNumber.Trim();
            // If user mistakenly adds "+" or "00" prefix, remove it
            if (nationalNumber.StartsWith("+"))
                nationalNumber = nationalNumber.Substring(1);

            if (nationalNumber.StartsWith("00"))
                nationalNumber = nationalNumber.Substring(2);
            // Remove country code prefix if user included it by mistake
            if (nationalNumber.StartsWith(CountryCode.Replace("+", "")))
                nationalNumber = nationalNumber.Substring(CountryCode.Replace("+", "").Length);
            // Map calling code to region
            var callingCodeToRegion = new Dictionary<string, string>
            {
                { "+1", "US" },
                { "+44", "GB" },
                { "+91", "IN" },
                { "+33", "FR" },
                { "+49", "DE" },
                { "+81", "JP" },
                { "+61", "AU" },
                { "+39", "IT" },
                { "+34", "ES" },
                { "+7", "RU" },
                { "+86", "CN" },
                { "+55", "BR" },
                { "+27", "ZA" },
                { "+82", "KR" },
                { "+31", "NL" },
                { "+46", "SE" },
                { "+41", "CH" },
                { "+34", "ES" },
                { "+90", "TR" },
                { "+351", "PT" },
                { "+30", "GR" },
                { "+351", "PT" },
                { "+65", "SG" },
                { "+62", "ID" },
                { "+60", "MY" },
                { "+63", "PH" },
                { "+52", "MX" },
                { "+34", "ES" },
                { "+48", "PL" },
                { "+420", "CZ" },
                { "+351", "PT" },
                { "+358", "FI" },
                { "+353", "IE" },
                { "+354", "IS" },
                { "+36", "HU" },
                { "+420", "CZ" },
                { "+351", "PT" },
                { "+358", "FI" },
                { "+353", "IE" },
                { "+354", "IS" },
                { "+36", "HU" }
            };
            if (!callingCodeToRegion.TryGetValue(CountryCode, out var regionCode))
            {
                results.Add(new ValidationResult("Unsupported country code.", new[] { nameof(CountryCode) }));
                return results;
            }
            try
            {
                // Parse expects only the national number and the region code
                var parsedNumber = phoneUtil.Parse(nationalNumber, regionCode);
                if (!phoneUtil.IsValidNumber(parsedNumber))
                {
                    results.Add(new ValidationResult("Please enter a valid phone number.", new[] { nameof(PhoneNumber) }));
                }
            }
            catch (NumberParseException)
            {
                results.Add(new ValidationResult("Invalid phone number format.", new[] { nameof(PhoneNumber) }));
            }
            return results;
        }
        // Static method to provide country codes list for dropdowns
        public static List<SelectListItem> GetCountryCodes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "+1", Text = "United States (+1)" },
                new SelectListItem { Value = "+44", Text = "United Kingdom (+44)" },
                new SelectListItem { Value = "+91", Text = "India (+91)" },
                new SelectListItem { Value = "+33", Text = "France (+33)" },
                new SelectListItem { Value = "+49", Text = "Germany (+49)" },
                new SelectListItem { Value = "+81", Text = "Japan (+81)" },
                new SelectListItem { Value = "+61", Text = "Australia (+61)" },
                new SelectListItem { Value = "+39", Text = "Italy (+39)" },
                new SelectListItem { Value = "+34", Text = "Spain (+34)" },
                new SelectListItem { Value = "+7", Text = "Russia (+7)" },
                new SelectListItem { Value = "+86", Text = "China (+86)" },
                new SelectListItem { Value = "+55", Text = "Brazil (+55)" },
                new SelectListItem { Value = "+27", Text = "South Africa (+27)" },
                new SelectListItem { Value = "+82", Text = "South Korea (+82)" },
                new SelectListItem { Value = "+31", Text = "Netherlands (+31)" },
                new SelectListItem { Value = "+46", Text = "Sweden (+46)" },
                new SelectListItem { Value = "+41", Text = "Switzerland (+41)" },
                new SelectListItem { Value = "+90", Text = "Turkey (+90)" },
                new SelectListItem { Value = "+351", Text = "Portugal (+351)" },
                new SelectListItem { Value = "+30", Text = "Greece (+30)" },
                new SelectListItem { Value = "+65", Text = "Singapore (+65)" },
                new SelectListItem { Value = "+62", Text = "Indonesia (+62)" },
                new SelectListItem { Value = "+60", Text = "Malaysia (+60)" },
                new SelectListItem { Value = "+63", Text = "Philippines (+63)" },
                new SelectListItem { Value = "+52", Text = "Mexico (+52)" },
                new SelectListItem { Value = "+48", Text = "Poland (+48)" },
                new SelectListItem { Value = "+420", Text = "Czech Republic (+420)" },
                new SelectListItem { Value = "+358", Text = "Finland (+358)" },
                new SelectListItem { Value = "+353", Text = "Ireland (+353)" },
                new SelectListItem { Value = "+354", Text = "Iceland (+354)" },
                new SelectListItem { Value = "+36", Text = "Hungary (+36)" },
            };
        }
    }
}
