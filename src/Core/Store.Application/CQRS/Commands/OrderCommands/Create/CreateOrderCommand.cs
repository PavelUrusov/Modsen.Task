using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Commands.OrderCommands.Create;

public record CreateOrderCommand(IEnumerable<OrderItemDetails> OrderDetails) : IRequest<ResponseBase>
{

    public Guid UserId { get; set; }

}

public record OrderItemDetails(int ProductId, int Quantity);