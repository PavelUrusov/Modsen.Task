using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Application.CQRS.Commands.OrderCommands.Create;
using Store.Application.CQRS.Queries.OrdersQueries.ReadRange;
using Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;
using Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;
using Store.WebApi.Common.Dtos;

namespace Store.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{

    private readonly IValidator<CreateOrderCommand> _createValidator;
    private readonly IValidator<ReadOrdersQuery> _getOrdersValidator;
    private readonly IValidator<ReadUserOrdersQuery> _getUserOrdersValidator;
    private readonly IValidator<ReadOrderQuery> _getOrderValidator;
    private readonly ILogger<OrderController> _logger;
    private readonly IMediator _mediator;

    public OrderController(
        ILogger<OrderController> logger,
        IValidator<CreateOrderCommand> createValidator,
        IValidator<ReadOrdersQuery> getOrdersValidator,
        IValidator<ReadUserOrdersQuery> getUserOrdersValidator,
        IValidator<ReadOrderQuery> getOrderValidator,
        IMediator mediator)
    {
        _logger = logger;
        _createValidator = createValidator;
        _getOrdersValidator = getOrdersValidator;
        _getUserOrdersValidator = getUserOrdersValidator;
        _getOrderValidator = getOrderValidator;
        _mediator = mediator;
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        var command = new CreateOrderCommand(orderDto.OrderDetails)
        {
            UserId = Guid.Parse(User!.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        };

        var validationResult = await _createValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"CreateOrder - Validation failed: {validationResult.Errors}");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] int? take, [FromQuery] int? skip)
    {
        var query = new ReadOrdersQuery(take, skip);
        var validationResult = await _getOrdersValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"GetOrders - Validation failed: {validationResult.Errors}");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpGet("User/{userId}/Orders")]
    public async Task<IActionResult> GetUserOrders([FromRoute] Guid userId, [FromQuery] int? take, [FromQuery] int? skip)
    {
        var query = new ReadUserOrdersQuery(take, skip, userId);
        var validationResult = await _getUserOrdersValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"GetUserOrders - Validation failed: {validationResult.Errors}");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GerOrder([FromRoute] int id)
    {
        var query = new ReadOrderQuery(id);
        var validationResult = await _getOrderValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"GetOrder - Validation failed: {validationResult.Errors}");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

}