using LSM.SsoService.Application.Command.Handlers.Auth;
using LSM.SsoService.Application.Command.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LSM.SsoService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : SsoControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand command,
        [FromServices] RegisterCommandHandler handler,
        CancellationToken ct = default
    )
    {
        var result = await handler.HandleAsync(command, ct);

        return result.IsSuccess
            ? CreatedAtAction("Register", null)
            : Error(result.Error);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        [FromServices] LoginCommandHandler handler,
        CancellationToken ct = default
    )
    {
        var result = await handler.HandleAsync(command, ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : Error(result.Error);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> RequestResetPassword(
        [FromBody] RequestResetPasswordCommand command,
        [FromServices] RequestResetPasswordCommandHandler handler,
        CancellationToken ct = default
    )
    {
        var result = await handler.HandleAsync(command, ct);

        return result.IsSuccess
            ? Accepted()
            : Error(result.Error);
    }
}