using FluentValidation;
using Store.Application.CQRS.Queries.ProductQueries.Read.Single;

namespace Store.WebApi.Common.Validation.ProductController;

public class ReadSingleProductQueryValidation : AbstractValidator<ReadSingleProductQuery>
{
    public ReadSingleProductQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}