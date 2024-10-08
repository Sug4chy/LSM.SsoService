using CSharpFunctionalExtensions;

namespace LSM.SsoService.Application.Common.Results;

public readonly struct EmptyResult : IUnitResult<Error>
{
    public bool IsFailure { get; }
    public bool IsSuccess { get; }
    public Error Error { get; }

    private EmptyResult(bool isSuccess, Error error)
    {
        IsFailure = isSuccess is false;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static EmptyResult Success()
        => new(true, default);

    public static EmptyResult Failure(Error error)
        => new(false, error);
}