using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.OrderCommands.Create;

internal class CreateOrderValidator : IValidationHandler<CreateOrderCommand>
{

    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public CreateOrderValidator(IUserRepository userRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<ValidationResult> Validate(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.ReadAsync(request.UserId, cancellationToken);

        if (user == null)
            return ValidationResult.Fail($"The user with that name - {request.UserId} already exists");

        foreach (var orderDetail in request.OrderDetails)
        {
            var product = await _productRepository.ReadAsync(orderDetail.ProductId, cancellationToken);

            if (product == null)
                return ValidationResult.Fail($"A product with this id - {orderDetail.ProductId} doesn't exist");

            if (product.Quantity < orderDetail.Quantity)
                return ValidationResult.Fail(
                    $"The quantity {orderDetail.Quantity} of product with ID {orderDetail.ProductId} " +
                    $"in the order is less than in stock {product.Quantity}");
        }

        return ValidationResult.Success;
    }

}