using Store.Application.CQRS.Commands.OrderCommands.Create;

namespace Store.WebApi.Common.Dtos;

public record CreateOrderDto(IEnumerable<OrderItemDetails> OrderDetails);