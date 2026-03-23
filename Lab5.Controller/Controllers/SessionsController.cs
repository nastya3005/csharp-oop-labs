using Lab5.Application.Interfaces.Services;
using Lab5.Controller.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controller.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUserSession([FromBody] CreateUserSessionRequest request)
    {
        try
        {
            Guid sessionId = await _sessionService.CreateUserSessionAsync(
                request.AccountNumber,
                request.PinCode);

            return Ok(new { SessionId = sessionId });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("admin")]
    public async Task<IActionResult> CreateAdminSession([FromBody] CreateAdminSessionRequest request)
    {
        try
        {
            Guid sessionId = await _sessionService.CreateAdminSessionAsync(request.AdminPassword);
            return Ok(new { SessionId = sessionId });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("{sessionId}")]
    public Task<IActionResult> CloseSession(Guid sessionId)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}