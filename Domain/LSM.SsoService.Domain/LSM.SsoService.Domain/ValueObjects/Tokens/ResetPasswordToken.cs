using LSM.SsoService.Domain.Entities;

namespace LSM.SsoService.Domain.ValueObjects.Tokens;

public sealed class ResetPasswordToken : Token
{
    public string Token { get; }
    public DateTime TokenExpiryDate { get; }
    public Guid UserId { get; init; }
    public User? User { get; init; }

    private ResetPasswordToken(string token, DateTime tokenExpiryDate)
    {
        Token = token;
        TokenExpiryDate = tokenExpiryDate;
    }

    public static ResetPasswordToken Create(
        string token,
        DateTime expiryDate)
        => new(
            token,
            expiryDate
        );

    public ResetPasswordToken ForUser(User user)
    {
        user.ResetPasswordToken = new ResetPasswordToken(Token, TokenExpiryDate)
        {
            User = user,
            UserId = user.Id
        };

        return user.ResetPasswordToken;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Token;
        yield return TokenExpiryDate;
    }
}