using FluentValidation;
using Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;

namespace Store.WebApi.Common.Validation.OrderController;

public class ReadOrderQueryValidation : AbstractValidator<ReadOrderQuery>
{

    public ReadOrderQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}