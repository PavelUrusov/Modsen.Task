using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadSingle;

internal class ReadProductHandler : IRequestHandler<ReadProductQuery, ResponseBase>, ILoggingBehavior
{

    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ReadProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.ReadAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ReadProductResponse>(product);

        return response;
    }

}