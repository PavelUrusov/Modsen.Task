using FluentValidation;
using Store.Application.CQRS.Queries.ProductQueries.ReadSingle;

namespace Store.WebApi.Common.Validation.ProductController;

public class ReadProductQueryValidation : AbstractValidator<ReadProductQuery>
{

    public ReadProductQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }

}