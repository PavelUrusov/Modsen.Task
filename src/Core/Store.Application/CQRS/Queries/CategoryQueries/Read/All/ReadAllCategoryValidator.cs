using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.All;

public class ReadAllCategoryValidator : IValidationHandler<ReadAllCategoryValidator>
{
    public Task<ValidationResult> Validate(ReadAllCategoryValidator request, CancellationToken cancellationToken)
    {
        return new Task<ValidationResult>(() => ValidationResult.Success);
    }
}