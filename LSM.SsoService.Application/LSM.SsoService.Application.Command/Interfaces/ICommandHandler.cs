using CSharpFunctionalExtensions;

namespace LSM.SsoService.Application.Command.Interfaces;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand, TResult> : ICommandHandler<TCommand>
    where TCommand : ICommand<TResult>
    where TResult : class
{
    new Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken ct = default);
}