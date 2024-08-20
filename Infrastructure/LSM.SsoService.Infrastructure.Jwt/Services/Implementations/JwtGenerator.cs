using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Infrastructure.Jwt.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LSM.SsoService.Infrastructure.Jwt.Services.Implementations;

internal sealed class JwtGenerator(
    IOptions<JwtConfiguration> jwtOptions
) : IJwtGenerator
{
    private readonly JwtConfiguration _configuration = jwtOptions.Value;

    public string GenerateForUser(User user)
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];

        var jwt = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(_configuration.ExpirationTime),
            signingCredentials: GetSigningCredentials(_configuration.Key)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static SigningCredentials GetSigningCredentials(string key)
        => new(
            GetSymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );

    private static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        => new(Encoding.UTF8.GetBytes(key));
}