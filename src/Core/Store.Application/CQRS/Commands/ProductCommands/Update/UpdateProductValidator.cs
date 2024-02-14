using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.ProductCommands.Update;

internal class UpdateProductValidator : IValidationHandler<UpdateProductCommand>
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public UpdateProductValidator(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ValidationResult> Validate(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var isProductExist = await _productRepository.ReadAsync(request.Id, cancellationToken);

        if (isProductExist == null)
            ValidationResult.Fail($"A category with this id - {request.Id} doesn't exist");

        if (!string.IsNullOrEmpty(request.Name))
        {
            var nameIsFree = await _productRepository.ReadByNameAsync(request.Name, cancellationToken);

            if (nameIsFree != null && nameIsFree.Id != request.Id)
                ValidationResult.Fail($"A category with this name - {request.Name} already exist");
        }

        if (request.NewCategoryIds != null && request.NewCategoryIds.Any())
            foreach (var id in request.NewCategoryIds)
                if (await _categoryRepository.ReadAsync(id, cancellationToken) == null)
                    ValidationResult.Fail($"A category with this id - {id} doesn't exist");

        if (request.NewCategoryIds != null)
        {
            var categories = await _categoryRepository.ReadManyAsync(request.NewCategoryIds, cancellationToken);

            if (categories.Count() != request.NewCategoryIds.Count())
                ValidationResult.Fail("A category with this ids doesn't exits");
        }

        return ValidationResult.Success;
    }

}