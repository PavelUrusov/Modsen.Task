using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadSingle;

internal class ReadProductValidator : IValidationHandler<ReadProductQuery>
{

    private readonly IProductRepository _repository;

    public ReadProductValidator(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(ReadProductQuery request, CancellationToken cancellationToken)
    {
        return await _repository.ReadAsync(request.Id, cancellationToken) == null
            ? ValidationResult.Fail($"A product with this id - {request.Id} doesn't exist")
            : ValidationResult.Success;
    }

}