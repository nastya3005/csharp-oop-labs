using Lab5.Application.Interfaces.Repositories;
using Lab5.Domain.Entities;
using Lab5.Infrastructure.Data;

namespace Lab5.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly InMemoryDataContext _context;

    public TransactionRepository(InMemoryDataContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(
            _context.Transactions.FirstOrDefault(t => t.Id == id));
    }

    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId)
    {
        return await Task.FromResult(
            _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList());
    }

    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await Task.CompletedTask;
    }
}