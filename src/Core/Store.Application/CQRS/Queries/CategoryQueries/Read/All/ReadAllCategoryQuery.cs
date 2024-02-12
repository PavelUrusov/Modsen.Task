using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.All;

public record ReadAllCategoryQuery : IRequest<ResponseBase>;