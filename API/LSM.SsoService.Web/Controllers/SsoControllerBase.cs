using LSM.SsoService.Application.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace LSM.SsoService.Web.Controllers;

public abstract class SsoControllerBase : ControllerBase
{
    [NonAction]
    protected IActionResult Error(Error error)
    {
        int statusCode = error.ErrorGroup switch
        {
            ErrorGroup.Validation => StatusCodes.Status400BadRequest,
            ErrorGroup.AlreadyExists => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(
            type: string.Empty,
            detail: error.Message,
            instance: null,
            statusCode: statusCode,
            title: error.ErrorGroup.ToString()
        );
    }
}