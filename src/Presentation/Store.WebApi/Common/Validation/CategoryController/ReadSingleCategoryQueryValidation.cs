using FluentValidation;
using Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

namespace Store.WebApi.Common.Validation.CategoryController;

public class ReadSingleCategoryQueryValidation : AbstractValidator<ReadSingleCategoryQuery>
{
    public ReadSingleCategoryQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}