using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.CategoryCommands.Delete;

internal class DeleteCategoryValidator : IValidationHandler<DeleteCategoryCommand>
{

    private readonly ICategoryRepository _repository;

    public DeleteCategoryValidator(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadAsync(request.Id, cancellationToken);

        return category == null
            ? ValidationResult.Fail($"A category with this id - {request.Id} doesn't exist")
            : ValidationResult.Success;
    }

}