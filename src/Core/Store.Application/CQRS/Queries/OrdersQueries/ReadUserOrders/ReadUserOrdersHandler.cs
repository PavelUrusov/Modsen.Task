using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;

internal class ReadUserOrdersHandler : IRequestHandler<ReadUserOrdersQuery, ResponseBase>, ILoggingBehavior

{

    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public ReadUserOrdersHandler(IMapper mapper, IOrderRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseBase> Handle(ReadUserOrdersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Order> orders;

        if (request.Take.HasValue && request.Skip.HasValue)
            orders = await _repository.ReadUserOrdersAsync(request.UserId, request.Take.Value, request.Skip.Value);
        else
            orders = await _repository.ReadUserOrdersAsync(request.UserId);

        var response = _mapper.Map<ReadUserOrdersResponse>(orders);

        return response;
    }

}