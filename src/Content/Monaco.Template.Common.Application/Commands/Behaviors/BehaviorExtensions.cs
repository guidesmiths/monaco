﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Monaco.Template.Common.Application.Commands.Contracts;

namespace Monaco.Template.Common.Application.Commands.Behaviors;

public static class BehaviorExtensions
{
    public static IServiceCollection RegisterPreCommandProcessorBehaviors(this IServiceCollection services, Assembly assembly)
    {
        //Gets the CommandBase derived classes
        var commandBaseTypes = assembly.GetTypes()
                                       .Where(x => x.BaseType == typeof(CommandBase))
                                       .ToList();
        //And adds the corresponding scoped behaviors for all the detected commands (for both existing and validation checks)
        commandBaseTypes.ForEach(t => services.AddScoped(typeof(IPipelineBehavior<,>).MakeGenericType(t, typeof(ICommandResult)),
                                                         typeof(CommandValidationExistsBehavior<>).MakeGenericType(t))
                                              .AddScoped(typeof(IPipelineBehavior<,>).MakeGenericType(t, typeof(ICommandResult)),
                                                         typeof(CommandValidationBehavior<>).MakeGenericType(t)));
        //Gets the CommandBases<T> derived classes
        var commandBaseResultTypes = assembly.GetTypes()
                                             .Where(x => (x.BaseType?.IsGenericType ?? false) && x.BaseType?.GetGenericTypeDefinition() == typeof(CommandBase<>))
                                             .ToList();
        //And adds the corresponding scoped behavior for all the detected commands (only for validation checks)
        commandBaseResultTypes.ForEach(t =>
                                       {
                                           var tResult = t.BaseType!.GenericTypeArguments.First();
                                           services.AddScoped(typeof(IPipelineBehavior<,>).MakeGenericType(t, typeof(ICommandResult<>).MakeGenericType(tResult)),
                                                              typeof(CommandValidationExistsBehavior<,>).MakeGenericType(t, tResult))
                                                   .AddScoped(typeof(IPipelineBehavior<,>).MakeGenericType(t, typeof(ICommandResult<>).MakeGenericType(tResult)),
                                                              typeof(CommandValidationBehavior<,>).MakeGenericType(t, tResult));
                                       });
        return services;
    }
}