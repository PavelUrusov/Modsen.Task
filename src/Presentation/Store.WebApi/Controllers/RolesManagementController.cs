using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Common;
using Store.Auth.Common.DTO;
using Store.Auth.Interfaces;
using Store.WebApi.Common.Dtos;

namespace Store.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesManagementController : ControllerBase
{

    private readonly ILogger<RolesManagementController> _logger;
    private readonly IValidator<UpdateUserRoleCredentials> _updateUserRoleValidator;

    private readonly IUserRoleService _userRoleService;

    public RolesManagementController(
        IUserRoleService userRoleService,
        IValidator<UpdateUserRoleCredentials> updateUserRoleValidator,
        ILogger<RolesManagementController> logger)
    {
        _userRoleService = userRoleService;
        _updateUserRoleValidator = updateUserRoleValidator;
        _logger = logger;
    }

    [Authorize(Policy = nameof(Roles.Admin))]
    [HttpPut("User/{id}")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] Guid id, [FromBody] UpdateUserRoleDto updateUserRole)
    {
        var credentials = new UpdateUserRoleCredentials(updateUserRole.Role, id);
        var validationResult = await _updateUserRoleValidator.ValidateAsync(credentials);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"UpdateUserRole - Validation failed: {validationResult.Errors}");

            return BadRequest(validationResult.Errors);
        }

        var response = await _userRoleService.UpdateUserRoleAsync(credentials);

        return StatusCode((int)response.StatusCode, response);
    }
}