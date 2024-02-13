using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.CategoryCommands.Delete;

internal class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ResponseBase>, ILoggingBehavior
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBase> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadAsync(request.Id, cancellationToken);
        await _repository.DeleteAsync(category!, cancellationToken);
        return ResponseBase.Success();
    }
}