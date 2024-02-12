using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.CQRS.CategoryCommands.Create;
using Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

namespace Store.WebApi.Controllers;

[Route("StoreAPI/[controller]")]
[ApiController]
public class DevController : ControllerBase
{
    private readonly IMediator _mediator;

    public DevController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> HiPoint()
    {
        var response = await _mediator.Send(new ReadSingleCategoryQuery(1));
        return StatusCode((int)response.StatusCode, response);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        var response = await _mediator.Send(new CreateCategoryCommand("TestCategoryName", "TestCategoryDescription"));
        return StatusCode((int)response.StatusCode, response);
    }
}