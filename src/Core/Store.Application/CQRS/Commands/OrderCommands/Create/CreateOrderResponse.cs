using Store.Application.Common;

namespace Store.Application.CQRS.Commands.OrderCommands.Create;

public record CreateOrderResponse(int CreatedId) : ResponseBase;