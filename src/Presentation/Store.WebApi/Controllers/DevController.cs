using Microsoft.AspNetCore.Mvc;

namespace Store.WebApi.Controllers;

[Route("StoreAPI/[controller]")]
[ApiController]
public class DevController : ControllerBase
{
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> HiPoint()
    {
        return Ok("Hello world!");
    }
}