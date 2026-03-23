namespace Lab5.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }

    public Guid? AccountId { get; private set; }

    public bool IsAdmin { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool IsActive => DateTime.UtcNow < ExpiresAt;

    public Session(Guid accountId, int timeoutMinutes = 30)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("Account ID cannot be empty", nameof(accountId));

        Id = Guid.NewGuid();
        AccountId = accountId;
        IsAdmin = false;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.AddMinutes(timeoutMinutes);
    }

    public Session(bool isAdmin, int timeoutMinutes = 30)
    {
        Id = Guid.NewGuid();
        AccountId = null;
        IsAdmin = true;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.AddMinutes(timeoutMinutes);
    }

    private Session()
    {
        AccountId = null;
    }

    public bool Validate()
    {
        return IsActive;
    }

    public void Refresh(int additionalMinutes = 30)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot refresh expired session");

        ExpiresAt = DateTime.UtcNow.AddMinutes(additionalMinutes);
    }

    public string GetSessionType()
    {
        return IsAdmin ? "Admin" : "User";
    }
}