using FluentValidation;
using Store.Application.CQRS.Queries.ProductQueries.Read.Range;

namespace Store.WebApi.Common.Validation.ProductController;

public class ReadRangeProductQueryValidation : AbstractValidator<ReadRangeProductQuery>
{
    public ReadRangeProductQueryValidation()
    {
        RuleFor(x => x.Take)
            .GreaterThan(0).WithMessage("Take must be greater than zero");

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to zero");

        RuleFor(x => x.MaxPrice)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("MaxPrice must be greater than zero")
            .PrecisionScale(18, 2, false).WithMessage("MaxPrice must have precision of 18 digits, with 2 decimal places")
            .When(x => x.MaxPrice != null);

        RuleFor(x => x.MinPrice)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("MinPrice must be greater than zero")
            .PrecisionScale(18, 2, false).WithMessage("MinPrice must have precision of 18 digits, with 2 decimal places")
            .When(x => x.MinPrice != null);

        RuleFor(x => x.CategoryIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("CategoryIds cannot be empty")
            .When(x => x.CategoryIds != null);
    }
}