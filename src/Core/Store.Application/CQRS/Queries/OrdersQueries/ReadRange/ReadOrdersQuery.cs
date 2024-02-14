using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadRange;

public record ReadOrdersQuery(int? Take, int? Skip) : IRequest<ResponseBase>;