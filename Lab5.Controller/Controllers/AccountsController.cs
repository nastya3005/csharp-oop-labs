using Lab5.Application.DTOs;
using Lab5.Application.Interfaces.Services;
using Lab5.Controller.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controller.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ISessionService _sessionService;

    public AccountsController(
        IAccountService accountService,
        ISessionService sessionService)
    {
        _accountService = accountService;
        _sessionService = sessionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
    {
        try
        {
            Guid accountId = await _accountService.CreateAccountAsync(dto);
            return Ok(new { AccountId = accountId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{accountId}/balance")]
    public async Task<IActionResult> GetBalance(
        Guid accountId,
        [FromHeader(Name = "SessionId")] Guid sessionId)
    {
        try
        {
            bool isValidSession = await _sessionService.ValidateSessionAsync(sessionId);
            if (!isValidSession)
                return Unauthorized(new { Error = "Invalid session" });

            decimal balance = await _accountService.GetBalanceAsync(accountId);
            return Ok(new { Balance = balance });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("{accountId}/withdraw")]
    public async Task<IActionResult> Withdraw(
        Guid accountId,
        [FromBody] WithdrawRequest request,
        [FromHeader(Name = "SessionId")] Guid sessionId)
    {
        try
        {
            bool isValidSession = await _sessionService.ValidateSessionAsync(sessionId);
            if (!isValidSession)
                return Unauthorized(new { Error = "Invalid session" });

            await _accountService.WithdrawAsync(accountId, request.Amount);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(
        Guid accountId,
        [FromBody] DepositRequest request,
        [FromHeader(Name = "SessionId")] Guid sessionId)
    {
        try
        {
            bool isValidSession = await _sessionService.ValidateSessionAsync(sessionId);
            if (!isValidSession)
                return Unauthorized(new { Error = "Invalid session" });

            await _accountService.DepositAsync(accountId, request.Amount);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{accountId}/transactions")]
    public async Task<IActionResult> GetTransactions(
        Guid accountId,
        [FromHeader(Name = "SessionId")] Guid sessionId)
    {
        try
        {
            bool isValidSession = await _sessionService.ValidateSessionAsync(sessionId);
            if (!isValidSession)
                return Unauthorized(new { Error = "Invalid session" });

            IEnumerable<Domain.Entities.Transaction> transactions = await _accountService.GetTransactionHistoryAsync(accountId);
            return Ok(transactions);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}