using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Range;

public record ReadRangeCategoryQuery(int Take, int Skip) : IRequest<ResponseBase>;