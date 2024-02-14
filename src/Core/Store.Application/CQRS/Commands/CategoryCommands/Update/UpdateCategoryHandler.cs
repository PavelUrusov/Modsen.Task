using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.CategoryCommands.Update;

internal class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ResponseBase>, ILoggingBehavior
{

    private readonly ICategoryRepository _repository;

    public UpdateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBase> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updCategory = await _repository.ReadAsync(request.Id, cancellationToken) ??
                          throw new InvalidOperationException($"Product with ID {request.Id} not found.");

        if (!string.IsNullOrEmpty(request.Name))
            updCategory.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Description))
            updCategory.Description = request.Description;

        await _repository.UpdateAsync(updCategory, cancellationToken);

        return new UpdateCategoryResponse(updCategory.Id);
    }

}