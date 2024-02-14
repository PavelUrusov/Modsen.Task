using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadRange;

public class ReadProductsValidator : IValidationHandler<ReadProductsQuery>
{

    public Task<ValidationResult> Validate(ReadProductsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ValidationResult.Success);
    }

}