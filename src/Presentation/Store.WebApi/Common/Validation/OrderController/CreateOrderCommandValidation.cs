using FluentValidation;
using Store.Application.CQRS.Commands.OrderCommands.Create;

namespace Store.WebApi.Common.Validation.OrderController;

public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{

    public CreateOrderCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("User ID must be provided.");

        RuleFor(x => x.OrderDetails)
            .NotEmpty().WithMessage("Order must contain at least one item.");

        RuleForEach(x => x.OrderDetails)
            .SetValidator(new OrderItemDetailsValidator());
    }

}

public class OrderItemDetailsValidator : AbstractValidator<OrderItemDetails>
{

    public OrderItemDetailsValidator()
    {
        RuleFor(detail => detail.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(detail => detail.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }

}