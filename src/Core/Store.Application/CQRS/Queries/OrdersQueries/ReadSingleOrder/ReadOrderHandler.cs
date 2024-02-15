using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;

public class ReadOrderHandler : IRequestHandler<ReadOrderQuery, ResponseBase>, ILoggingBehavior
{

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public ReadOrderHandler(IOrderRepository orderRepository,IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<ResponseBase> Handle(ReadOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.ReadAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ReadOrderResponse>(order);

        return response;
    }

}