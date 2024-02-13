using FluentValidation;
using Store.Application.CQRS.Commands.ProductCommands.Update;

namespace Store.WebApi.Common.Validation.ProductController;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Name is required")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(255).WithMessage("Name length cannot exceed 255 characters");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(1023).WithMessage("Description length cannot exceed 1023 characters")
            .When(x => x.Description != null);

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0).WithMessage("Price cannot be zero")
            .GreaterThan(0).WithMessage("Price must be greater than zero")
            .PrecisionScale(18, 2, false).WithMessage("Price must have precision of 18 digits, with 2 decimal places");

        RuleFor(x => x.Quantity)
            .GreaterThan(-1).WithMessage("Quantity must be greater than or equal to zero");

        RuleFor(x => x.NewCategoryIds)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("CategoryIds cannot be null")
            .NotEmpty().WithMessage("CategoryIds cannot be empty");
    }
}