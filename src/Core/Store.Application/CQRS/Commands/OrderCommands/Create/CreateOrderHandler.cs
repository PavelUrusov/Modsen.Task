using System.Data;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.TransactionService;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Commands.OrderCommands.Create;

internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseBase>, ILoggingBehavior
{

    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ITransactionService _transactionService;

    public CreateOrderHandler(IOrderRepository orderRepository, ITransactionService transactionService,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _transactionService = transactionService;
        _productRepository = productRepository;
    }

    public async Task<ResponseBase> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.OrderDetails.Select(od => od.ProductId).ToList();
        var products = await _productRepository.ReadManyAsync(productIds, cancellationToken);
        var productsDictionary = products.ToDictionary(p => p.Id);

        foreach (var detail in request.OrderDetails)
        {
            var product = productsDictionary[detail.ProductId];

            if (product.Quantity < detail.Quantity)
                throw new InvalidOperationException(
                    $"Product with ID {detail.ProductId} is not available in sufficient quantity.");
        }

        var orderId = await _transactionService.ExecuteInTransactionAsync(async () =>
        {
            var order = new Order
            {
                UserId = request.UserId,
                CreationDate = DateTime.UtcNow
            };

            var orderItems = request.OrderDetails.Select(od => new OrderItem
            {
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                Order = order
            }).ToList();

            order.OrderItems = orderItems;

            foreach (var item in order.OrderItems)
            {
                var product = productsDictionary[item.ProductId];
                product.Quantity -= item.Quantity;
            }

            await _orderRepository.CreateAsync(order, cancellationToken);
            await _productRepository.UpdateRangeAsync(products, cancellationToken);

            return order.Id;
        }, IsolationLevel.Serializable, cancellationToken);

        return new CreateOrderResponse(orderId);
    }

}