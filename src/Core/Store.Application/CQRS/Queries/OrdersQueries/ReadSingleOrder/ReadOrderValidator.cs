using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;

internal class ReadOrderValidator : IValidationHandler<ReadOrderQuery>
{

    private readonly IOrderRepository _repository;

    public ReadOrderValidator(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(ReadOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.ReadAsync(request.Id, cancellationToken);

        return order == null
            ? ValidationResult.Fail($"A order with this id - {request.Id} does not exist")
            : ValidationResult.Success;
    }

}