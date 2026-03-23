using Lab5.Domain.Enums;

namespace Lab5.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }

    public TransactionType Type { get; private set; }

    public decimal Amount { get; private set; }

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Transaction(Guid accountId, TransactionType type, decimal amount, string description)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Type = type;
        Amount = amount;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    private Transaction() { }
}