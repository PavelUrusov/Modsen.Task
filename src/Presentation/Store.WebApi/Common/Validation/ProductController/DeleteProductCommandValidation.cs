using FluentValidation;
using Store.Application.CQRS.Commands.ProductCommands.Delete;

namespace Store.WebApi.Common.Validation.ProductController;

public class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
{

    public DeleteProductCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }

}