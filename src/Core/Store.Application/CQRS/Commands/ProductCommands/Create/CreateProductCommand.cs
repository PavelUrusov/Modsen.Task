using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Create;

public record CreateProductCommand(string Name, string? Description, decimal Price, int Quantity, IEnumerable<int> CategoryIds)
    : IRequest<ResponseBase>;