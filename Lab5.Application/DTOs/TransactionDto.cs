using Lab5.Domain.Enums;

namespace Lab5.Application.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}