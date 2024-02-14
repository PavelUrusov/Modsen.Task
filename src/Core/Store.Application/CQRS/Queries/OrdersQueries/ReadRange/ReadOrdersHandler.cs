using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadRange;

internal class ReadOrdersHandler : IRequestHandler<ReadOrdersQuery, ResponseBase>, ILoggingBehavior
{

    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public ReadOrdersHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadOrdersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Order> orders;

        if (request.Skip.HasValue && request.Take.HasValue)
            orders = await _repository.ReadRangeAsync(request.Skip.Value, request.Take.Value, o => o.Id,
                cancellationToken: cancellationToken);

        else
            orders = await _repository.ReadAllAsync(cancellationToken);

        var response = _mapper.Map<ReadOrdersResponse>(orders);

        return response;
    }

}