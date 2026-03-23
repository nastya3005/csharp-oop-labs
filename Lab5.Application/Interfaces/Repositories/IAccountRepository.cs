using Lab5.Domain.Entities;

namespace Lab5.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);

    Task<Account?> GetByAccountNumberAsync(string accountNumber);

    Task<bool> ExistsAsync(string accountNumber);

    Task AddAsync(Account account);

    Task UpdateAsync(Account account);
}