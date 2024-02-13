using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Range;

public class ReadRangeCategoryValidator : IValidationHandler<ReadRangeCategoryValidator>
{
    public Task<ValidationResult> Validate(ReadRangeCategoryValidator request, CancellationToken cancellationToken)
    {
        return new Task<ValidationResult>(() => ValidationResult.Success);
    }
}