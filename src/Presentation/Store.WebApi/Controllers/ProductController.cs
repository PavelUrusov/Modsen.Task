using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Application.CQRS.Commands.ProductCommands.Create;
using Store.Application.CQRS.Commands.ProductCommands.Delete;
using Store.Application.CQRS.Commands.ProductCommands.Update;
using Store.Application.CQRS.Queries.ProductQueries.ReadRange;
using Store.Application.CQRS.Queries.ProductQueries.ReadSingle;
using Store.WebApi.Common.Dtos;

namespace Store.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly IValidator<CreateProductCommand> _createValidator;
    private readonly IValidator<DeleteProductCommand> _deleteValidator;
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<ReadProductsQuery> _readRangeValidator;
    private readonly IValidator<ReadProductQuery> _readValidator;
    private readonly IValidator<UpdateProductCommand> _updateValidator;

    public ProductController(IMediator mediator,
        ILogger<ProductController> logger,
        IValidator<CreateProductCommand> createValidator,
        IValidator<ReadProductsQuery> readRangeValidator,
        IValidator<ReadProductQuery> readValidator,
        IValidator<UpdateProductCommand> updateValidator,
        IValidator<DeleteProductCommand> deleteValidator)
    {
        _mediator = mediator;
        _logger = logger;
        _createValidator = createValidator;
        _readRangeValidator = readRangeValidator;
        _readValidator = readValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var validationResult = await _createValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Product creation failed validation.");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ReadProductsQuery query)
    {
        var validationResult = await _readRangeValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Product range query failed validation.");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var query = new ReadProductQuery(id);
        var validationResult = await _readValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Single product query failed validation.");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto dto)
    {
        var command = new UpdateProductCommand(id, dto.Name, dto.Description, dto.Price, dto.Quantity, dto.NewCategoryIds);
        var validationResult = await _updateValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Product update failed validation.");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var command = new DeleteProductCommand(id);
        var validationResult = await _deleteValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Product deletion failed validation.");

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

}