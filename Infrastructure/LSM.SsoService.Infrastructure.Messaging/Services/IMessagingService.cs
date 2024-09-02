using LSM.SsoService.Domain.ValueObjects;

namespace LSM.SsoService.Infrastructure.Messaging.Services;

public interface IMessagingService
{
    Task SendResetPasswordEmailAsync(Email email, string token, CancellationToken ct = default);
}