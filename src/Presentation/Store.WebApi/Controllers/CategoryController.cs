using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Application.CQRS.Commands.CategoryCommands.Create;
using Store.Application.CQRS.Commands.CategoryCommands.Delete;
using Store.Application.CQRS.Commands.CategoryCommands.Update;
using Store.Application.CQRS.Queries.CategoryQueries.Read.Range;
using Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

namespace Store.WebApi.Controllers;

[AllowAnonymous]
[Route("StoreAPI/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IValidator<CreateCategoryCommand> _createValidator;
    private readonly IValidator<DeleteCategoryCommand> _deleteValidator;
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<ReadRangeCategoryQuery> _readRangeValidator;
    private readonly IValidator<ReadSingleCategoryQuery> _readValidator;
    private readonly IValidator<UpdateCategoryCommand> _updateValidator;

    public CategoryController(IMediator mediator,
        ILogger<CategoryController> logger,
        IValidator<CreateCategoryCommand> createValidator,
        IValidator<ReadRangeCategoryQuery> readRangeValidator,
        IValidator<ReadSingleCategoryQuery> readValidator,
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

    [Authorize(Policy = nameof(Roles.Admin))]
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request)
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
    public async Task<IActionResult> ReadRange([FromQuery] ReadRangeCategoryQuery request)
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
    public async Task<IActionResult> Read([FromQuery] ReadSingleCategoryQuery request)
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
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand request)
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
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand request)
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