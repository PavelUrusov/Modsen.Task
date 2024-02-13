using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Auth.Common.DTO;
using Store.Auth.Interfaces;

namespace Store.WebApi.Controllers;

[Route("StoreAPI/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly IValidator<SignInCredentials> _signInValidator;
    private readonly IValidator<SignUpCredentials> _signUpValidator;

    public AuthController(IAuthService authService,
        IValidator<SignUpCredentials> signUpValidator,
        IValidator<SignInCredentials> signInValidator,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _signUpValidator = signUpValidator;
        _signInValidator = signInValidator;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCredentials credentials)
    {
        var requestName = nameof(SignUp);
        var result = await _signUpValidator.ValidateAsync(credentials);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid credentials in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _authService.SignUpAsync(credentials);
        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignIn([FromBody] SignInCredentials credentials)
    {
        var requestName = nameof(SignIn);
        var result = await _signInValidator.ValidateAsync(credentials);
        if (!result.IsValid)
        {
            _logger.LogError($"{requestName} - Invalid credentials in the request.");
            return BadRequest(result.Errors);
        }

        var response = await _authService.SignInAsync(credentials);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPost]
    [Route("[action]")]
    public new async Task<IActionResult> SignOut()
    {
        var response = await _authService.SignOutAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _authService.RefreshTokenAsync();
        return StatusCode((int)response.StatusCode, response);
    }
}