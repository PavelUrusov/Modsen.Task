using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Delete;

public record DeleteProductResponse(int DeletedId) : ResponseBase;