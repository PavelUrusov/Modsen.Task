using FluentValidation;
using Store.Application.CQRS.Queries.CategoryQueries.ReadSingle;

namespace Store.WebApi.Common.Validation.CategoryController;

public class ReadCategoryQueryValidation : AbstractValidator<ReadCategoryQuery>
{

    public ReadCategoryQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }

}