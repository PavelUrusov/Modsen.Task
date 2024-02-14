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
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(255).WithMessage("Name length cannot exceed 255 characters")
            .When(x => x.Name != null);

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(1023).WithMessage("Description length cannot exceed 1023 characters")
            .When(x => x.Description != null);

        RuleFor(x => x)
            .Must(x => !(string.IsNullOrEmpty(x.Name) && string.IsNullOrEmpty(x.Description))).WithMessage("All data is empty");
    }

}