using LSM.SsoService.Domain.Entities;

namespace LSM.SsoService.Application.Command.Services;

public interface ITokenSource
{
    string ResetPasswordTokenFor(User user);
}