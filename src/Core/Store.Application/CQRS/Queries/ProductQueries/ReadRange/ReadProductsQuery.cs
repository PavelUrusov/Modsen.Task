using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadRange;

public record ReadProductsQuery(
    int? Take,
    int? Skip,
    string? Name,
    decimal? MinPrice,
    decimal? MaxPrice,
    bool AvailableInStock,
    IEnumerable<int>? CategoryIds)
    : IRequest<ResponseBase>;