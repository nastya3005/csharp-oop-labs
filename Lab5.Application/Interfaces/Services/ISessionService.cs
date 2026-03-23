namespace Lab5.Application.Interfaces.Services;

public interface ISessionService
{
    Task<Guid> CreateUserSessionAsync(string accountNumber, string pinCode);

    Task<Guid> CreateAdminSessionAsync(string adminPassword);

    Task<bool> ValidateSessionAsync(Guid sessionId);

    Task<Guid?> GetAccountIdFromSessionAsync(Guid sessionId);

    Task<bool> IsAdminSessionAsync(Guid sessionId);
}