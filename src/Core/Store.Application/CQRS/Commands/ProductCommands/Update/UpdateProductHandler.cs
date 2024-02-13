using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Commands.ProductCommands.Update;

internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseBase>, ILoggingBehavior
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ResponseBase> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updateProduct = await _productRepository.ReadAsync(request.Id, cancellationToken);
        updateProduct!.Description = request.Description;
        updateProduct.Name = request.Name;
        updateProduct.Price = request.Price;
        updateProduct.Quantity = request.Quantity;
        updateProduct.Categories = request.NewCategoryIds.Select(id => new Category { Id = id });

        await _productRepository.UpdateAsync(updateProduct, cancellationToken);
        return ResponseBase.Success();
    }
}