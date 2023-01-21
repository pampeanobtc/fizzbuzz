using System.Reflection;
using FizzBuzz.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FizzBuzz.Application;

public static class ApplicationDependencyInjection
{
        
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // Mediator
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // Mediator behaviours
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}