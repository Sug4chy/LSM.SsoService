using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Common.Results;

namespace LSM.SsoService.Application.Command.Interfaces;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : class
{
    Task<Result<TResult, Error>> HandleAsync(TCommand command, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<EmptyResult> HandleAsync(TCommand command, CancellationToken ct = default);
}