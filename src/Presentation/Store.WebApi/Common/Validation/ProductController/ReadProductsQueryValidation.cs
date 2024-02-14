using FluentValidation;
using Store.Application.CQRS.Queries.ProductQueries.ReadRange;

namespace Store.WebApi.Common.Validation.ProductController;

public class ReadProductsQueryValidation : AbstractValidator<ReadProductsQuery>
{

    public ReadProductsQueryValidation()
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