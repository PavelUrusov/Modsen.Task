using System.Net;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Commands.ProductCommands.Create;

internal class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseBase>, ILoggingBehavior
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ResponseBase> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categories = new List<Category>(request.CategoryIds.Count());
        foreach (var id in request.CategoryIds)
        {
            var category = await _categoryRepository.ReadAsync(id, cancellationToken);
            categories.Add(category!);
        }

        var newProduct = new Product
        {
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            Categories = categories
        };
        await _productRepository.CreateAsync(newProduct, cancellationToken);
        return ResponseBase.Success(HttpStatusCode.Created);
    }
}