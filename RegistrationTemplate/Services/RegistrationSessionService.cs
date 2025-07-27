using Microsoft.EntityFrameworkCore;
using RegistrationTemplate.Data;
using RegistrationTemplate.Models;
using RegistrationTemplate.Services.IServices;

namespace RegistrationTemplate.Services
{
    public class RegistrationSessionService : IRegistrationSessionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RegistrationSessionService> _logger;

        public RegistrationSessionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<RegistrationSessionService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<RegistrationSession> CreateNewSessionAsync()
        {
            var newSession = new RegistrationSession
            {
                RegistrationId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                CountryCode = string.Empty,
                PhoneNumber = string.Empty,
                RecoveryEmail = string.Empty
            };
            _context.RegistrationSessions.Add(newSession);
            await _context.SaveChangesAsync();
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("RegistrationId", newSession.RegistrationId.ToString(), options);
            return newSession;
        }

        public async Task DeleteSessionAsync(RegistrationSession session)
        {
            var sessionUser = await _context.RegistrationSessions.FirstOrDefaultAsync(reg => reg.RegistrationId == session.RegistrationId);
            if (sessionUser != null)
            {
                _context.RegistrationSessions.Remove(sessionUser); //DELETED
                await _context.SaveChangesAsync();
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("RegistrationId");
            }
        }

        public async Task<RegistrationSession> GetOrCreateSessionAsync(string? registrationId)
        {
            try
            {
                if (!string.IsNullOrEmpty(registrationId) && Guid.TryParse(registrationId, out var regId))
                {
                    var existing = await _context.RegistrationSessions.FindAsync(regId);
                    if (existing != null)
                        return existing;
                }
                return await CreateNewSessionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get or create registration session.");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
