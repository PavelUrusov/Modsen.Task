using Store.Application.Common;

namespace Store.Application.CQRS.Commands.CategoryCommands.Update;

public record UpdateCategoryResponse(int UpdatedId) : ResponseBase;