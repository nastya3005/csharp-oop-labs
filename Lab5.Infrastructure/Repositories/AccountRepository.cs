using Lab5.Application.Interfaces.Repositories;
using Lab5.Domain.Entities;
using Lab5.Infrastructure.Data;

namespace Lab5.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly InMemoryDataContext _context;

    public AccountRepository(InMemoryDataContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(_context.Accounts.FirstOrDefault(a => a.Id == id));
    }

    public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        return await Task.FromResult(_context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber));
    }

    public async Task<bool> ExistsAsync(string accountNumber)
    {
        return await Task.FromResult(_context.Accounts.Any(a => a.AccountNumber == accountNumber));
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Account account)
    {
        Account? existingAccount = await GetByIdAsync(account.Id);
        if (existingAccount != null)
        {
            _context.Accounts.Remove(existingAccount);
            _context.Accounts.Add(account);
        }

        await Task.CompletedTask;
    }
}