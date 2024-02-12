using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

public record ReadSingleCategoryQuery(int Id) : IRequest<ResponseBase>;