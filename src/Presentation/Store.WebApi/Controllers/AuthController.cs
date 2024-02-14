using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Auth.Common.DTO;
using Store.Auth.Interfaces;

namespace Store.WebApi.Controllers;

[Route("api/[controller]")]
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
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] SignUpCredentials credentials)
    {
        var validationResult = await _signUpValidator.ValidateAsync(credentials);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Register - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _authService.SignUpAsync(credentials);

        return StatusCode((int)response.StatusCode, response);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] SignInCredentials credentials)
    {
        var validationResult = await _signInValidator.ValidateAsync(credentials);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Login - Validation failed: {Errors}", validationResult.Errors);

            return BadRequest(validationResult.Errors);
        }

        var response = await _authService.SignInAsync(credentials);

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        var response = await _authService.SignOutAsync();

        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize(Policy = nameof(Roles.User))]
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _authService.RefreshTokenAsync();

        return StatusCode((int)response.StatusCode, response);
    }

}