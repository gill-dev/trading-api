using Microsoft.Extensions.Logging;
using trading.application.Abstractions.Messaging;
using trading.domain;

namespace trading.application.Decorators;

internal sealed class LoggingCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    private readonly ICommandHandler<TCommand, TResponse> _innerHandler;
    private readonly ILogger<LoggingCommandHandler<TCommand, TResponse>> _logger;

    public LoggingCommandHandler(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<LoggingCommandHandler<TCommand, TResponse>> logger)
    {
        _innerHandler = innerHandler;
        _logger = logger;
    }

    public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        var commandName = typeof(TCommand).Name;

        _logger.LogInformation("Processing command {Command}", commandName);

        var result = await _innerHandler.Handle(command, cancellationToken);

        if (result.IsSuccess)
            _logger.LogInformation("Completed command {Command}", commandName);
        else
            _logger.LogWarning("Command {Command} failed: {Error}", commandName, result.Error.Message);

        return result;
    }
}