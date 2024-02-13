using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.ProductCommands.Delete;

internal class DeleteProductValidator : IValidationHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ValidationResult> Validate(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.ReadAsync(request.Id, cancellationToken);
        if (product == null)
            return ValidationResult.Fail($"A product with this id - {request.Id} doesn't exist");

        return product.OrderItems.Any()
            ? ValidationResult.Fail("The product can't be deleted because it's associated with one or more orders.")
            : ValidationResult.Success;
    }
}