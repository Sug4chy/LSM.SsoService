using CSharpFunctionalExtensions;
using LSM.SsoService.Domain.Abstractions;
using LSM.SsoService.Domain.Enums;
using LSM.SsoService.Domain.ValueObjects;

namespace LSM.SsoService.Domain.Entities;

public sealed class User : Entity<Guid>, IHasEmail
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public Email? Email { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Patronymic { get; set; }
    public Role Role { get; set; }
    
    public ICollection<Session>? Sessions { get; set; }
}