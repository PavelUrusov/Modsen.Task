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
        var updateProduct = await _productRepository.ReadAsync(request.Id, cancellationToken) ??
                            throw new InvalidOperationException($"Product with ID {request.Id} not found.");

        if (!string.IsNullOrEmpty(request.Description))
            updateProduct.Description = request.Description;

        if (!string.IsNullOrEmpty(request.Name))
            updateProduct.Name = request.Name;

        if (request.Price.HasValue)
            updateProduct.Price = request.Price.Value;

        if (request.Quantity.HasValue)
            updateProduct.Quantity = request.Quantity.Value;

        if (request.NewCategoryIds != null)
            updateProduct.Categories = request.NewCategoryIds.Select(id => new Category { Id = id }).ToList();

        await _productRepository.UpdateAsync(updateProduct, cancellationToken);

        return new UpdateProductResponse(updateProduct.Id);
    }

}