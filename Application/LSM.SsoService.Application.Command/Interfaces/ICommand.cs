using LSM.SsoService.Application.Common.Results;

namespace LSM.SsoService.Application.Command.Interfaces;

public interface ICommand<TResult>;

public interface ICommand : ICommand<EmptyResult>;