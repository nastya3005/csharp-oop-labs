using Lab5.Application.Common.Interfaces;
using Lab5.Application.Interfaces.Repositories;
using Lab5.Application.Interfaces.Services;
using Lab5.Domain.Entities;

namespace Lab5.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ISettingsProvider _settingsProvider;

    public SessionService(
        ISessionRepository sessionRepository,
        IAccountRepository accountRepository,
        ISettingsProvider settingsProvider)
    {
        _sessionRepository = sessionRepository;
        _accountRepository = accountRepository;
        _settingsProvider = settingsProvider;
    }

    public async Task<Guid> CreateUserSessionAsync(string accountNumber, string pinCode)
    {
        Account? account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null || !account.VerifyPinCode(pinCode))
            throw new UnauthorizedAccessException("Invalid account number or PIN code");

        var session = new Session(account.Id, _settingsProvider.GetSessionTimeoutMinutes());
        await _sessionRepository.AddAsync(session);
        return session.Id;
    }

    public async Task<Guid> CreateAdminSessionAsync(string adminPassword)
    {
        if (adminPassword != _settingsProvider.GetAdminPassword())
            throw new UnauthorizedAccessException("Invalid admin password");

        var session = new Session(true, _settingsProvider.GetSessionTimeoutMinutes());
        await _sessionRepository.AddAsync(session);
        return session.Id;
    }

    public async Task<bool> ValidateSessionAsync(Guid sessionId)
    {
        Session? session = await _sessionRepository.GetByIdAsync(sessionId);
        return session != null && session.IsActive;
    }

    public async Task<Guid?> GetAccountIdFromSessionAsync(Guid sessionId)
    {
        Session? session = await _sessionRepository.GetByIdAsync(sessionId);
        return session?.AccountId;
    }

    public async Task<bool> IsAdminSessionAsync(Guid sessionId)
    {
        Session? session = await _sessionRepository.GetByIdAsync(sessionId);
        return session?.IsAdmin ?? false;
    }
}