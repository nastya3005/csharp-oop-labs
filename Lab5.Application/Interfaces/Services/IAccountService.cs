using Lab5.Application.DTOs;
using Lab5.Domain.Entities;

namespace Lab5.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Guid> CreateAccountAsync(CreateAccountDto dto);

    Task<decimal> GetBalanceAsync(Guid accountId);

    Task WithdrawAsync(Guid accountId, decimal amount);

    Task DepositAsync(Guid accountId, decimal amount);

    Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(Guid accountId);
}