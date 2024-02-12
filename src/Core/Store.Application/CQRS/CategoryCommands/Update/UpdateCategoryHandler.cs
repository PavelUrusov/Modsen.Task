using MediatR;
using Store.Application.Common;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.CategoryCommands.Update;

internal class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ResponseBase>
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBase> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updCategory = await _repository.ReadAsync(request.Id, cancellationToken);
        updCategory!.Name = request.Name;
        updCategory.Description = request.Description;
        await _repository.UpdateAsync(updCategory, cancellationToken);
        return ResponseBase.Success();
    }
}