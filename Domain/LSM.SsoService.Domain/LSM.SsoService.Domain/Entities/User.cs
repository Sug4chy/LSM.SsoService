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
    public DateTime BirthDate { get; set; }

    public ICollection<Session>? Sessions { get; set; }

    public static User Create(
        string username,
        string password,
        string name,
        string surname,
        DateTime birthDate,
        Maybe<string> patronymic,
        Maybe<Email> email)
        => new()
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = password,
            Email = email.HasValue
                ? email.Value
                : null,
            Name = name,
            Surname = surname,
            Patronymic = patronymic.HasValue
                ? patronymic.Value
                : null,
            Role = Role.Reader,
            BirthDate = birthDate
        };
}