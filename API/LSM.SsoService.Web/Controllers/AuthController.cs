using LSM.SsoService.Application.Command.Handlers.Auth;
using LSM.SsoService.Application.Command.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LSM.SsoService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : ControllerBase
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
            ? Ok(result) 
            : BadRequest(result);
    }
}