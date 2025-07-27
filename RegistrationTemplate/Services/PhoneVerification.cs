using RegistrationTemplate.Services.IServices;
using System.Collections.Concurrent;

namespace RegistrationTemplate.Services
{
    public class PhoneVerification : IPhoneVerification
    {
        private static ConcurrentDictionary<string, VerificationEntry> _verificationCodes = new();
        public async Task<string> SendVerificationCodeAsync(string phoneNumber)
        {
            var code = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(5);
            _verificationCodes[phoneNumber] = new VerificationEntry
            {
                Code = code,
                Expiry = expiry,
                Attempts = 0
            };
            await Task.Delay(100); // Simulate SMS delay
            Console.WriteLine($"Verification code for {phoneNumber}: {code}");
            return code;
        }

        public bool ValidateCode(string phoneNumber, string code)
        {
            if (_verificationCodes.TryGetValue(phoneNumber, out var entry))
            {
                if (DateTime.UtcNow > entry.Expiry)
                {
                    _verificationCodes.TryRemove(phoneNumber, out _);
                    return false; // Code expired
                }
                if (entry.Attempts >= 5)
                {
                    return false; // Too many attempts
                }
                if (entry.Code == code)
                {
                    _verificationCodes.TryRemove(phoneNumber, out _); // Remove on success
                    return true;
                }
                else
                {
                    entry.Attempts++;
                    return false;
                }
            }
            return false;
        }

        public class VerificationEntry
        {
            public string Code { get; set; } = null!;
            public DateTime Expiry { get; set; }
            public int Attempts { get; set; }
        }
    }
}
