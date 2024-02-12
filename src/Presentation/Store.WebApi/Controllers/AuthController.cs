using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Auth.Common.DTO;
using Store.Auth.Interfaces;

namespace Store.WebApi.Controllers;

[AllowAnonymous]
[Route("StoreAPI/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCredentials credentials)
    {
        var response = await _authService.SignUpAsync(credentials);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignIn([FromBody] SignInCredentials credentials)
    {
        var response = await _authService.SignInAsync(credentials);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpPost]
    [Route("[action]")]
    public new async Task<IActionResult> SignOut()
    {
        var response = await _authService.SignOutAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _authService.RefreshTokenAsync();
        return StatusCode((int)response.StatusCode, response);
    }
}