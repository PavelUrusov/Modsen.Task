using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Update;

public record UpdateProductResponse(int UpdatedId) : ResponseBase;