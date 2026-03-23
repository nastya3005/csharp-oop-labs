using Lab5.Application.Interfaces.Repositories;
using Lab5.Application.Services;
using Lab5.Domain.Entities;
using Lab5.Domain.Enums;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class TestApi
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly AccountService _accountService;

    public TestApi()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _transactionRepository = Substitute.For<ITransactionRepository>();

        _accountService = new AccountService(_accountRepository, _transactionRepository);
    }

    [Fact]
    public async Task WithdrawAsync_WithSufficientBalance_ShouldUpdateBalance()
    {
        var accountId = Guid.NewGuid();
        var account = new Account("1234567890", "1234");
        account.Deposit(1000);

        _accountRepository.GetByIdAsync(accountId)
            .Returns(Task.FromResult<Account?>(account));

        decimal amountToWithdraw = 500m;

        await _accountService.WithdrawAsync(accountId, amountToWithdraw);

        Assert.Equal(500, account.Balance);

        await _accountRepository.Received(1)
            .UpdateAsync(Arg.Is<Account>(a => a.Balance == 500));

        await _transactionRepository.Received(1)
            .AddAsync(Arg.Is<Transaction>(t =>
                t.Type == TransactionType.Withdraw &&
                t.Amount == amountToWithdraw));
    }

    [Fact]
    public async Task WithdrawAsync_WithInsufficientBalance_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var account = new Account("1234567890", "1234");
        account.Deposit(100);

        _accountRepository.GetByIdAsync(accountId)
            .Returns(Task.FromResult<Account?>(account));

        decimal amountToWithdraw = 500m;

        InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _accountService.WithdrawAsync(accountId, amountToWithdraw));

        Assert.Contains("Insufficient funds", exception.Message, StringComparison.OrdinalIgnoreCase);

        Assert.Equal(100, account.Balance);

        await _accountRepository.DidNotReceive().UpdateAsync(Arg.Any<Account>());

        await _transactionRepository.DidNotReceive().AddAsync(Arg.Any<Transaction>());
    }

    [Fact]
    public async Task DepositAsync_ShouldUpdateBalance()
    {
        var accountId = Guid.NewGuid();
        var account = new Account("1234567890", "1234");
        account.Deposit(1000);

        _accountRepository.GetByIdAsync(accountId)
            .Returns(Task.FromResult<Account?>(account));

        decimal amountToDeposit = 500m;

        await _accountService.DepositAsync(accountId, amountToDeposit);

        Assert.Equal(1500, account.Balance);

        await _accountRepository.Received(1)
            .UpdateAsync(Arg.Is<Account>(a => a.Balance == 1500));

        await _transactionRepository.Received(1)
            .AddAsync(Arg.Is<Transaction>(t =>
                t.Type == TransactionType.Deposit && t.Amount == amountToDeposit));
    }
}