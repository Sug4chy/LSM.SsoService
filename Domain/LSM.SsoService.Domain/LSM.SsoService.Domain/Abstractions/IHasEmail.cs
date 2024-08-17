using LSM.SsoService.Domain.ValueObjects;

namespace LSM.SsoService.Domain.Abstractions;

public interface IHasEmail
{
    public Email? Email { get; set; }
}