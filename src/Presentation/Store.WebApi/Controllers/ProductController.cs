using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Application.CQRS.Commands.ProductCommands.Create;
using Store.Application.CQRS.Commands.ProductCommands.Delete;
using Store.Application.CQRS.Commands.ProductCommands.Update;
using Store.Application.CQRS.Queries.ProductQueries.Read.Range;
using Store.Application.CQRS.Queries.ProductQueries.Read.Single;

namespace Store.WebApi.Controllers;

[Route("StoreAPI/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IValidator<CreateProductCommand> _createValidator;
    private readonly IValidator<DeleteProductCommand> _deleteValidator;
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<ReadRangeProductQuery> _readRangeValidator;
    private readonly IValidator<ReadSingleProductQuery> _readValidator;
    private readonly IValidator<UpdateProductCommand> _updateValidator;

    public ProductController(IMediator mediator,
        IValidator<CreateProductCommand> createValidator,
        IValidator<ReadRangeProductQuery> readRangeValidator,
        IValidator<ReadSingleProductQuery> readValidator,
        IValidator<UpdateProductCommand> updateValidator,
        IValidator<DeleteProductCommand> deleteValidator,
        ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _createValidator = createValidator;
        _readRangeValidator = readRangeValidator;
        _readValidator = readValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
        _logger = logger;
    }

    [Authorize(Policy = nameof(Roles.Admin))]
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
    {
        var requestName = nameof(Create);
        var result = await _createValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid data in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> ReadRange([FromQuery] ReadRangeProductQuery request)
    {
        var requestName = nameof(ReadRange);
        var result = await _readRangeValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid data in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> Read([FromQuery] ReadSingleProductQuery request)
    {
        var requestName = nameof(Read);
        var result = await _readValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid data in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Admin))]
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
    {
        var requestName = nameof(Update);
        var result = await _updateValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid data in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Admin))]
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand request)
    {
        var requestName = nameof(Delete);
        var result = await _deleteValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid data in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
}