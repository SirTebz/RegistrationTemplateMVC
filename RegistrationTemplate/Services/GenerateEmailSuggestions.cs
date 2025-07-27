using Microsoft.EntityFrameworkCore;
using RegistrationTemplate.Data;
using RegistrationTemplate.Services.IServices;

namespace RegistrationTemplate.Services
{
    public class GenerateEmailSuggestions : IGenerateEmailSuggestions
    {
        private readonly ApplicationDbContext _context;
        public GenerateEmailSuggestions(ApplicationDbContext context)
        {
            _context = context;
        }
        // Asynchronously generates a list of unique email suggestions based on the base email provided
        // baseEmail: The base email to generate suggestions from (e.g., teboho.mokgosi@example.com)
        // count: The number of unique email suggestions to generate (default is 2)
        public async Task<List<string>> GenerateUniqueEmailsAsync(string baseEmail, int count = 2)
        {
            var suggestions = new List<string>();  // List to store email suggestions
            // If the base email is null or doesn't contain the '@' symbol, return an empty list
            if (string.IsNullOrEmpty(baseEmail) || !baseEmail.Contains("@"))
                return suggestions;
            // pranaya.rout@example.com
            // Split the base email into prefix and domain (e.g., "teboho.mokgosi" and "example.com")
            string emailPrefix = baseEmail.Split('@')[0];  // Extracts the part before '@'
            string emailDomain = baseEmail.Split('@')[1];  // Extracts the part after '@'
            string suggestion;  // Variable to store the generated suggestion
            // Continue generating suggestions until we have the desired number (specified by 'count')
            while (suggestions.Count < count)
            {
                do
                {
                    // Generate a random suggestion by appending a random number (100-999) to the prefix
                    // teboho.mokgosi124@example.com
                    suggestion = $"{emailPrefix}{new Random().Next(100, 999)}@{emailDomain}";
                    // Use AnyAsync to asynchronously check if the email already exists in the database
                    // Also ensure that the suggestion is not already in the suggestions list
                } while (await _context.Users.AnyAsync(u => u.Email == suggestion) || suggestions.Contains(suggestion));
                // Add the new unique suggestion to the list
                suggestions.Add(suggestion);
            }
            // Return the list of unique email suggestions
            return suggestions;
        }
    }
}
