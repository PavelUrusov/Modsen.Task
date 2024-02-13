using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Single;

internal class ReadSingleProductValidator : IValidationHandler<ReadSingleProductQuery>
{
    private readonly IProductRepository _repository;

    public ReadSingleProductValidator(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ValidationResult> Validate(ReadSingleProductQuery request, CancellationToken cancellationToken)
    {
        return await _repository.ReadAsync(request.Id, cancellationToken) == null
            ? ValidationResult.Fail($"A product with this id - {request.Id} doesn't exist")
            : ValidationResult.Success;
    }
}