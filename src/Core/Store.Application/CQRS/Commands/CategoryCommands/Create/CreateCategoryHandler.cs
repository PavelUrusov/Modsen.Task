using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Commands.CategoryCommands.Create;

internal class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ResponseBase>, ILoggingBehavior
{

    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBase> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await _repository.CreateAsync(newCategory, cancellationToken);

        return new CreateCategoryResponse(newCategory.Id);
    }

}