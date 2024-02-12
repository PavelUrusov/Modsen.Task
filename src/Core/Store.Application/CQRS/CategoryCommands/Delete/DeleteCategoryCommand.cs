using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.CategoryCommands.Delete;

public record DeleteCategoryCommand(int Id) : IRequest<ResponseBase>;