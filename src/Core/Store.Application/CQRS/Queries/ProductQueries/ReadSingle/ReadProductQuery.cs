using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadSingle;

public record ReadProductQuery(int Id) : IRequest<ResponseBase>;