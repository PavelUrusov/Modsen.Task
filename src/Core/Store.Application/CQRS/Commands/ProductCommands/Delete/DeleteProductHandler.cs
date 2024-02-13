using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Commands.ProductCommands.Delete;

internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseBase>, ILoggingBehavior
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ResponseBase> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var deleteProduct = await _productRepository.ReadAsync(request.Id, cancellationToken);
        await _productRepository.DeleteAsync(deleteProduct!, cancellationToken);
        return ResponseBase.Success();
    }
}