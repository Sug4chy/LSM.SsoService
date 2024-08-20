using LSM.SsoService.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

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
            ErrorGroup.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(
            detail: error.Message,
            instance: null,
            statusCode: statusCode,
            title: ReasonPhrases.GetReasonPhrase(statusCode)
        );
    }
}