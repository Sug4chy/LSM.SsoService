using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Application.Command.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandValidation(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<IValidationAssemblyMarker>();
}

file interface IValidationAssemblyMarker;