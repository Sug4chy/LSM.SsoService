using System.Security.Cryptography;
using System.Text;
using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects.Tokens;
using LSM.SsoService.Infrastructure.Persistence.Context;
using LSM.SsoService.Infrastructure.Tokens.Configuration;

namespace LSM.SsoService.Infrastructure.Tokens.Services.Implementations;

internal sealed class ResetPasswordTokenSource(
    TokenSourceConfiguration tokenSourceConfiguration,
    IDataContext dataContext
) : ITokenSource<ResetPasswordToken>
{
    public ResetPasswordToken ResetPasswordTokenFor(User user)
    {
        string tokenBase = DateTimeOffset.Now.ToString("yyyy-MM-dd") + user.Id;

        byte[] bytes = Encoding.UTF8.GetBytes(tokenBase);
        byte[] hash = SHA512.HashData(bytes);
        string resetPasswordToken = Convert.ToBase64String(hash);

        var resultToken = ResetPasswordToken
            .Create(resetPasswordToken, DateTime.UtcNow.Add(tokenSourceConfiguration.ResetPasswordTokenExpirationPeriod))
            .ForUser(user);
        dataContext.ResetPasswordTokens.Add(resultToken);

        return resultToken;
    }
}