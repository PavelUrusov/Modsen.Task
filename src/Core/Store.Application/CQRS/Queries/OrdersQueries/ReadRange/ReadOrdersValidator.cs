using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadRange;

public class ReadOrdersValidator : IValidationHandler<ReadOrdersQuery>
{

    public Task<ValidationResult> Validate(ReadOrdersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ValidationResult.Success);
    }

}