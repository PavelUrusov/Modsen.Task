using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.CategoryCommands.Create;

internal class CreateCategoryValidator : IValidationHandler<CreateCategoryCommand>
{

    private readonly ICategoryRepository _repository;

    public CreateCategoryValidator(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadByNameAsync(request.Name, cancellationToken);

        return category != null
            ? ValidationResult.Fail($"A category with that name - {request.Name} already exists")
            : ValidationResult.Success;
    }

}