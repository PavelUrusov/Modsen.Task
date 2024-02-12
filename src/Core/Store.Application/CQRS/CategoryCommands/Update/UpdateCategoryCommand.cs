using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.CategoryCommands.Update;

public record UpdateCategoryCommand(int Id, string Name, string Description) : IRequest<ResponseBase>;