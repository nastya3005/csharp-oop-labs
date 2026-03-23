using Lab5.Application.DTOs;
using Lab5.Application.Interfaces.Repositories;
using Lab5.Application.Interfaces.Services;
using Lab5.Domain.Entities;

namespace Lab5.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public AccountService(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<Guid> CreateAccountAsync(CreateAccountDto dto)
    {
        if (await _accountRepository.ExistsAsync(dto.AccountNumber))
            throw new ArgumentException($"Account with number {dto.AccountNumber} already exists");

        var account = new Account(dto.AccountNumber, dto.PinCode);
        await _accountRepository.AddAsync(account);
        return account.Id;
    }

    public async Task<decimal> GetBalanceAsync(Guid accountId)
    {
        Account? account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException($"Account with ID {accountId} not found");

        return account.Balance;
    }

    public async Task WithdrawAsync(Guid accountId, decimal amount)
    {
        Account? account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException($"Account with ID {accountId} not found");

        int transactionCountBefore = account.Transactions.Count;

        account.Withdraw(amount);

        await _accountRepository.UpdateAsync(account);

        IEnumerable<Transaction> newTransactions = account.Transactions.Skip(transactionCountBefore);

        foreach (Transaction? transaction in newTransactions)
        {
            await _transactionRepository.AddAsync(transaction);
        }
    }

    public async Task DepositAsync(Guid accountId, decimal amount)
    {
        Account? account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException($"Account with ID {accountId} not found");

        account.Deposit(amount);
        await _accountRepository.UpdateAsync(account);

        Transaction? lastTransaction = account.Transactions.LastOrDefault();
        if (lastTransaction != null)
            await _transactionRepository.AddAsync(lastTransaction);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(Guid accountId)
    {
        return await _transactionRepository.GetByAccountIdAsync(accountId);
    }
}