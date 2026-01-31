using Microsoft.Extensions.Logging;
using trading.application.Abstractions.Messaging;
using trading.domain;

namespace trading.application.Decorators;

internal sealed class LoggingQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly ILogger<LoggingQueryHandler<TQuery, TResponse>> _logger;
        private readonly IQueryHandler<TQuery, TResponse> _handler;

        public LoggingQueryHandler(ILogger<LoggingQueryHandler<TQuery, TResponse>> logger, IQueryHandler<TQuery, TResponse> handler)
        {
            _logger = logger;
            _handler = handler;
        }

        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var queryName = typeof(TQuery).Name;
            
            _logger.LogInformation("Query {queryName}", queryName);
            
            var result = await _handler.Handle(query, cancellationToken);
            
            if (result.IsSuccess)
                _logger.LogInformation("Query {queryName}", queryName);
            else
                _logger.LogWarning("Query {Query} failed: {Error}", queryName, result.Error);
            
            return result;
        }
}
