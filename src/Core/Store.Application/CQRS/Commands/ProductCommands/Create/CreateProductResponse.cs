using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Create;

public record CreateProductResponse(int CreatedId) : ResponseBase;