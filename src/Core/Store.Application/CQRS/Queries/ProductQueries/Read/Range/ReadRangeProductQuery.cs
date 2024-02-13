using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Range;

public record ReadRangeProductQuery(
    int Take,
    int Skip,
    string? Name,
    decimal? MinPrice,
    decimal? MaxPrice,
    bool AvailableInStock,
    IEnumerable<int>? CategoryIds)
    : IRequest<ResponseBase>;