using MediatR;
using Microsoft.Extensions.Logging;
using Store.Application.Common;

namespace Store.Application.CQRS.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResponseBase, new() where TRequest : IRequest<TResponse>
{

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        _logger.LogInformation($"Handling {requestName}.");
        var response = await next();
        _logger.LogInformation($"Finished handling {requestName} with code - {response.StatusCode}.");

        return response;
    }

}