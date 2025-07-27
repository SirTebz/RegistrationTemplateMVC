using RegistrationTemplate.Models;

namespace RegistrationTemplate.Services.IServices
{
    public interface IRegistrationSessionService
    {
        Task<RegistrationSession> GetOrCreateSessionAsync(string? registrationId);
        Task<RegistrationSession> CreateNewSessionAsync();
        Task SaveChangesAsync();
        Task DeleteSessionAsync(RegistrationSession session);
    }
}
