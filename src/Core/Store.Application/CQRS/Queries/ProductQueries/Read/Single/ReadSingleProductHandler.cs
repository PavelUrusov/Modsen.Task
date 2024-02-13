using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Single;

internal class ReadSingleProductHandler : IRequestHandler<ReadSingleProductQuery, ResponseBase>, ILoggingBehavior
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ReadSingleProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadSingleProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.ReadAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ReadSingleProductResponse>(product);
        return response;
    }
}