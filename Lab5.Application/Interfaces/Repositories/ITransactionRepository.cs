using Lab5.Domain.Entities;

namespace Lab5.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);

    Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);

    Task AddAsync(Transaction transaction);
}