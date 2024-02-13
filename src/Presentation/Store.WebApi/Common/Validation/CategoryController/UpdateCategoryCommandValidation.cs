using FluentValidation;
using Store.Application.CQRS.Commands.CategoryCommands.Update;

namespace Store.WebApi.Common.Validation.CategoryController;

public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidation()
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
    }
}