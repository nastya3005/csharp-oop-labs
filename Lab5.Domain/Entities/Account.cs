using Lab5.Domain.Enums;

namespace Lab5.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }

    public string AccountNumber { get; private set; }

    public string PinCodeHash { get; private set; }

    public decimal Balance { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public Account(string accountNumber, string pinCode)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));

        if (string.IsNullOrWhiteSpace(pinCode))
            throw new ArgumentException("PIN code cannot be empty", nameof(pinCode));

        if (pinCode.Length != 4 || !pinCode.All(char.IsDigit))
            throw new ArgumentException("PIN code must be exactly 4 digits", nameof(pinCode));

        Id = Guid.NewGuid();
        AccountNumber = accountNumber.Trim();
        PinCodeHash = HashPinCode(pinCode);
        Balance = 0;
        CreatedAt = DateTime.UtcNow;
    }

    private Account()
    {
        AccountNumber = string.Empty;
        PinCodeHash = string.Empty;
    }

    public void Withdraw(decimal amount, string description = "Cash withdrawal")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (Balance < amount)
        {
            throw new InvalidOperationException(
                $"Insufficient funds. Balance: {Balance:C}, requested: {amount:C}");
        }

        Balance -= amount;
        CreateTransaction(TransactionType.Withdraw, amount, description);
    }

    public void Deposit(decimal amount, string description = "Account deposit")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        Balance += amount;
        CreateTransaction(TransactionType.Deposit, amount, description);
    }

    public bool VerifyPinCode(string pinCode)
    {
        return PinCodeHash == HashPinCode(pinCode);
    }

    public IEnumerable<Transaction> GetRecentTransactions(int count = 10)
    {
        return Transactions.OrderByDescending(t => t.CreatedAt).Take(count);
    }

    private static string HashPinCode(string pinCode)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pinCode));
    }

    private void CreateTransaction(TransactionType type, decimal amount, string description)
    {
        var transaction = new Transaction(Id, type, amount, description);
        Transactions.Add(transaction);
    }
}