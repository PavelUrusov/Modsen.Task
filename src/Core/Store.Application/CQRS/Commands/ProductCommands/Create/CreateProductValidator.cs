using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.ProductCommands.Create;

internal class CreateProductValidator : IValidationHandler<CreateProductCommand>
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CreateProductValidator(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<ValidationResult> Validate(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.ReadByNameAsync(request.Name, cancellationToken);

        if (product != null)
            return ValidationResult.Fail($"A product with that name - {request.Name} already exists");

        foreach (var id in request.CategoryIds)
            if (await _categoryRepository.ReadAsync(id, cancellationToken) == null)
                return ValidationResult.Fail($"A category with this id - {id} doesn't exist");

        return ValidationResult.Success;
    }

}