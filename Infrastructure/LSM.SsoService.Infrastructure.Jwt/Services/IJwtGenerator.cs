using LSM.SsoService.Domain.Entities;

namespace LSM.SsoService.Infrastructure.Jwt.Services;

public interface IJwtGenerator
{
    string GenerateForUser(User user);
}