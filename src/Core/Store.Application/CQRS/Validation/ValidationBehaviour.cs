using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.Application.Common;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResponseBase, new() where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
    private readonly IValidationHandler<TRequest>? _validationHandler;

    public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger,
        IValidationHandler<TRequest> validationHandler)
    {
        _logger = logger;
        _validationHandler = validationHandler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType();
        if (_validationHandler == null)
        {
            _logger.LogWarning($"{requestName} doesn't have a validation handler configured.");
            return await next();
        }

        var result = await _validationHandler.Validate(request, cancellationToken);
        if (!result.IsSuccessful)
        {
            _logger.LogError($"Validation failed for {requestName}. Error: {result.Error}");
            return new TResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = result.Error };
        }

        _logger.LogInformation($"Validation successful for {requestName}.");
        return await next();
    }
}