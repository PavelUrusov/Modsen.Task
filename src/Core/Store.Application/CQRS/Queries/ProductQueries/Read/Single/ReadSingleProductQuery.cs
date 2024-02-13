using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Single;

public record ReadSingleProductQuery(int Id) : IRequest<ResponseBase>;