using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadSingle;

internal class ReadCategoryValidator : IValidationHandler<ReadCategoryQuery>
{

    private readonly ICategoryRepository _repository;

    public ReadCategoryValidator(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(ReadCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadAsync(request.Id, cancellationToken);

        return category == null
            ? ValidationResult.Fail($"A category with this id - {request.Id} does not exist")
            : ValidationResult.Success;
    }

}