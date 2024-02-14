using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;

public record ReadUserOrdersQuery(int? Take, int? Skip, Guid UserId) : IRequest<ResponseBase>;