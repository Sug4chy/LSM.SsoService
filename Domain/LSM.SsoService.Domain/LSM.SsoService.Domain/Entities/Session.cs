using CSharpFunctionalExtensions;

namespace LSM.SsoService.Domain.Entities;

public sealed class Session : Entity<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}