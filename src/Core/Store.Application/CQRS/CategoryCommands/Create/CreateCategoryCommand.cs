using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.CategoryCommands.Create;

public record CreateCategoryCommand(string Name, string Description) : IRequest<ResponseBase>;