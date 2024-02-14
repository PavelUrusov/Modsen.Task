using Store.Application.Common;

namespace Store.Application.CQRS.Commands.CategoryCommands.Create;

public record CreateCategoryResponse(int CreatedId) : ResponseBase;