using FluentValidation;
using Store.Application.CQRS.Queries.CategoryQueries.ReadRange;

namespace Store.WebApi.Common.Validation.CategoryController;

public class ReadCategoriesQueryValidation : AbstractValidator<ReadCategoriesQuery>
{

    public ReadCategoriesQueryValidation()
    {
        RuleFor(x => x.Take)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Take must be greater than zero")
            .When(x => x.Take.HasValue);

        RuleFor(x => x.Skip)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to zero")
            .When(x => x.Skip.HasValue);

        RuleFor(x => x)
            .Must(x => (x.Take.HasValue && x.Skip.HasValue) || (!x.Take.HasValue && !x.Skip.HasValue))
            .WithMessage("Either both Take and Skip must has value , or both should be empty.");
    }

}