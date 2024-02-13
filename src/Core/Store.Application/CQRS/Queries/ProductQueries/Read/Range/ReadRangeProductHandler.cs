using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Range;

internal class ReadRangeProductHandler : IRequestHandler<ReadRangeProductQuery, ResponseBase>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ReadRangeProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadRangeProductQuery request, CancellationToken cancellationToken)
    {
        var filters = ConfigureFilters(request);
        var products = await _productRepository.ReadRangeAsync(request.Skip, request.Take, p => p.Id, filters, cancellationToken);
        var response = _mapper.Map<ReadRangeProductResponse>(products);
        return response;
    }

    private IEnumerable<Expression<Func<Product, bool>>> ConfigureFilters(ReadRangeProductQuery request)
    {
        var filters = new List<Expression<Func<Product, bool>>>();
        if (request.AvailableInStock)
            filters.Add(p => p.Quantity > 0);

        if (!string.IsNullOrEmpty(request.Name))
            filters.Add(p => p.Name.ToLower().Contains(request.Name.ToLower()));

        if (request.MinPrice != null)
            filters.Add(p => p.Price >= request.MinPrice);

        if (request.MaxPrice != null)
            filters.Add(p => p.Price <= request.MaxPrice);

        if (request.CategoryIds != null && request.CategoryIds.Any())
            filters.Add(p => p.Categories.Any(c => request.CategoryIds.Contains(c.Id)));

        return filters;
    }
}