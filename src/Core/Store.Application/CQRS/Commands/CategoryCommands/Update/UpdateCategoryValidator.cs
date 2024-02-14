using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.CategoryCommands.Update;

internal class UpdateCategoryValidator : IValidationHandler<UpdateCategoryCommand>
{

    private readonly ICategoryRepository _repository;

    public UpdateCategoryValidator(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadAsync(request.Id, cancellationToken);

        if (category == null)
            return ValidationResult.Fail($"A category with this id - {request.Id} does not exist");

        var nameIsFree = await _repository.ReadByNameAsync(request.Name, cancellationToken);

        if (nameIsFree != null && request.Id != category.Id)
            return ValidationResult.Fail($"A category with this name - {request.Name} already exist");

        return ValidationResult.Success;
    }

}