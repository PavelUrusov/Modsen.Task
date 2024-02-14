using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadRange;

public record ReadCategoriesQuery(int? Take, int? Skip) : IRequest<ResponseBase>;