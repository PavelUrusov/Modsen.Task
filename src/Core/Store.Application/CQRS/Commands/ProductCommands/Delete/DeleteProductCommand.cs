using MediatR;
using Store.Application.Common;

namespace Store.Application.CQRS.Commands.ProductCommands.Delete;

public record DeleteProductCommand(int Id) : IRequest<ResponseBase>;