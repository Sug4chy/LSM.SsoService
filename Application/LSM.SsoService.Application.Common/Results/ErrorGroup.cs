namespace LSM.SsoService.Application.Common.Results;

public enum ErrorGroup : byte
{
    Validation = 0,
    AlreadyExists = 1,
    NotFound = 2,
}