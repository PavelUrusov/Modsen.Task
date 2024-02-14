using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Commands.CategoryCommands.Update;

public record UpdateCategoryCommand(int Id, string? Name, string? Description) : IRequest<ResponseBase>;