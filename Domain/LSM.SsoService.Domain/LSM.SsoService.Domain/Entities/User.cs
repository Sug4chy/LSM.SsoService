using CSharpFunctionalExtensions;
using LSM.SsoService.Domain.Enums;
using LSM.SsoService.Domain.ValueObjects;

namespace LSM.SsoService.Domain.Entities;

public sealed class User : Entity<Guid>
{
    public required Email Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Patronymic { get; set; }
    public Role Role { get; set; }
    public DateTime BirthDate { get; set; }

    public ICollection<Session>? Sessions { get; set; }

    public static User Create(
        string password,
        string name,
        string surname,
        DateTime birthDate,
        Email email,
        Maybe<string> patronymic)
        => new()
        {
            Id = Guid.NewGuid(),
            Password = password,
            Email = email,
            Name = name,
            Surname = surname,
            Patronymic = patronymic.HasValue
                ? patronymic.Value
                : null,
            Role = Role.Reader,
            BirthDate = birthDate
        };
}