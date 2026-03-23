using Lab5.Domain.Entities;

namespace Lab5.Infrastructure.Data;

public class InMemoryDataContext
{
    private readonly List<Account> _accounts = new();
    private readonly List<Transaction> _transactions = new();
    private readonly List<Session> _sessions = new();

    public ICollection<Account> Accounts => _accounts;

    public ICollection<Transaction> Transactions => _transactions;

    public ICollection<Session> Sessions => _sessions;

    public InMemoryDataContext()
    {
        InitializeTestData();
    }

    private void InitializeTestData()
    {
        var testAccount = new Account("1234567890", "1234");
        Accounts.Add(testAccount);
    }
}
