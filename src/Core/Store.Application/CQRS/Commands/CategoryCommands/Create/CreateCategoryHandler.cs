using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Commands.CategoryCommands.Create;

internal class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ResponseBase>, ILoggingBehavior
{
    private readonly ILogger<CreateCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository, ILogger<CreateCategoryCommand> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResponseBase> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category
        {
            Name = request.Name,
            Description = request.Description
        };
        await _repository.CreateAsync(newCategory, cancellationToken);
        return ResponseBase.Success(HttpStatusCode.Created);
    }
}