using System.Security.Cryptography;
using System.Text;
using LSM.SsoService.Application.Command.Configuration;
using LSM.SsoService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace LSM.SsoService.Application.Command.Services.Implementations;

internal sealed class TokenSource(
    IOptions<TokenSourceConfiguration> tokenSourceOptions
) : ITokenSource
{
    private readonly TokenSourceConfiguration _configuration = tokenSourceOptions.Value;

    public string ResetPasswordTokenFor(User user)
    {
        string tokenBase = DateTimeOffset.Now.ToString("yyyy-MM-dd") + user.Id;

        byte[] bytes = Encoding.UTF8.GetBytes(tokenBase);
        byte[] hash = SHA512.HashData(bytes);
        string resetPasswordToken = Convert.ToBase64String(hash);

        user.CurrentResetPasswordToken = resetPasswordToken;
        user.CurrentResetPasswordTokenExpiryDate = DateTime.UtcNow.Add(_configuration.ResetPasswordTokenExpirationPeriod);

        return resetPasswordToken;
    }
}