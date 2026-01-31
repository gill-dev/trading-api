using Microsoft.Extensions.DependencyInjection;
using trading.application.Abstractions.Messaging;
using trading.application.Decorators;

namespace trading.application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingQueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingCommandHandler<,>));
        return services;
    }
}