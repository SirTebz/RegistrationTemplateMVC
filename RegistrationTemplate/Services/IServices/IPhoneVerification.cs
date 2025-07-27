namespace RegistrationTemplate.Services.IServices
{
    public interface IPhoneVerification
    {
        Task<string> SendVerificationCodeAsync(string phoneNumber);
        bool ValidateCode(string phoneNumber, string code);
    }
}
