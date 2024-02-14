using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Application.CQRS.Commands.CategoryCommands.Create;
using Store.Application.CQRS.Commands.CategoryCommands.Delete;
using Store.Application.CQRS.Commands.CategoryCommands.Update;
using Store.Application.CQRS.Queries.CategoryQueries.ReadRange;
using Store.Application.CQRS.Queries.CategoryQueries.ReadSingle;
using Store.WebApi.Common.Dtos;

namespace Store.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    private readonly IValidator<CreateCategoryCommand> _createValidator;
    private readonly IValidator<DeleteCategoryCommand> _deleteValidator;
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<ReadCategoriesQuery> _readRangeValidator;
    private readonly IValidator<ReadCategoryQuery> _readValidator;
    private readonly IValidator<UpdateCategoryCommand> _updateValidator;

    public CategoryController(IMediator mediator,
        ILogger<CategoryController> logger,
        IValidator<CreateCategoryCommand> createValidator,
        IValidator<ReadCategoriesQuery> readRangeValidator,
        IValidator<ReadCategoryQuery> readValidator,
        IValidator<UpdateCategoryCommand> updateValidator,
        IValidator<DeleteCategoryCommand> deleteValidator)
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
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var validationResult = await _createValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("CreateCategory - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] int? take, [FromQuery] int? skip)
    {
        var request = new ReadCategoriesQuery(take, skip);
        var validationResult = await _readRangeValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            _logger.LogError("GetCategories - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(request);

        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] int id)
    {
        var query = new ReadCategoryQuery(id);
        var validationResult = await _readValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            _logger.LogError("GetCategory - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto dto)
    {
        var command = new UpdateCategoryCommand(id, dto.Name, dto.Description);
        var validationResult = await _updateValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("UpdateCategory - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.Employee))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var command = new DeleteCategoryCommand(id);
        var validationResult = await _deleteValidator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            _logger.LogError("DeleteCategory - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

}