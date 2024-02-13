using FluentValidation;
using Store.Application.CQRS.Commands.CategoryCommands.Delete;

namespace Store.WebApi.Common.Validation.CategoryController;

public class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}