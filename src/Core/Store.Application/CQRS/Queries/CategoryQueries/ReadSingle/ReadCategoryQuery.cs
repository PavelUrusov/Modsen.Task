using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadSingle;

public record ReadCategoryQuery(int Id) : IRequest<ResponseBase>;