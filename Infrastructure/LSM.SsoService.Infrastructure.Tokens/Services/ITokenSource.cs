using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects.Tokens;

namespace LSM.SsoService.Infrastructure.Tokens.Services;

public interface ITokenSource<out TToken>
    where TToken : Token
{
    TToken ResetPasswordTokenFor(User user);
}