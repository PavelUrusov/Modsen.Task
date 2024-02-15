using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;

public record ReadOrderQuery(int Id) : IRequest<ResponseBase>;