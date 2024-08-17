namespace LSM.SsoService.Application.Command.Interfaces;

public interface ICommand;

public interface ICommand<TResult> : ICommand
    where TResult : class;