using FluentValidation;
using Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;

namespace Store.WebApi.Common.Validation.OrderController;

public class ReadUserOrdersQueryValidator : AbstractValidator<ReadUserOrdersQuery>
{

    public ReadUserOrdersQueryValidator()
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

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("Id is required");
    }

}