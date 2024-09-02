using LSM.Common.Contracts;
using LSM.SsoService.Domain.ValueObjects;
using MassTransit;

namespace LSM.SsoService.Infrastructure.Messaging.Services.Implementations;

internal sealed class MessagingServiceClient(
    IPublishEndpoint publishEndpoint
) : IMessagingService
{
    public Task SendResetPasswordEmailAsync(Email email, string token, CancellationToken ct = default)
        => publishEndpoint.Publish(new SendResetPasswordEmail(email, token), ct);
}