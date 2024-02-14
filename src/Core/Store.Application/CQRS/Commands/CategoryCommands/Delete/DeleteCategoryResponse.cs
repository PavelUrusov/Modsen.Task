using Store.Application.Common;

namespace Store.Application.CQRS.Commands.CategoryCommands.Delete;

public record DeleteCategoryResponse(int DeletedId) : ResponseBase;