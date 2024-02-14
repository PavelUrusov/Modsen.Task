using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadRange;

public class ReadCategoriesValidator : IValidationHandler<ReadCategoriesValidator>
{

    public Task<ValidationResult> Validate(ReadCategoriesValidator request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ValidationResult.Success);
    }

}