using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Update;

public record UpdateProductCommand(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int Quantity,
    IEnumerable<int> NewCategoryIds) : IRequest<ResponseBase>;