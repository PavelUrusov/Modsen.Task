using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Store.Application.Common;
using Store.Application.Interfaces.TransactionService;
using Store.Auth.Common.DTO;
using Store.Auth.Interfaces;
using Store.Domain.Entities;

namespace Store.Auth.Services;

internal class UserRoleService : IUserRoleService
{

    private readonly IAuthorizationContext _authContext;
    private readonly ILogger<UserRoleService> _logger;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITransactionService _transactionService;
    private readonly UserManager<User> _userManager;

    public UserRoleService(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<UserRoleService> logger,
        IAuthorizationContext authContext, ITransactionService transactionService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _authContext = authContext;
        _transactionService = transactionService;
    }

    public async Task<ResponseBase> UpdateUserRoleAsync(UpdateUserRoleCredentials credentials)
    {
        var methodName = nameof(UpdateUserRoleAsync);
        var newRoleName = credentials.Role;
        var userId = credentials.UserId;

        _logger.LogInformation(
            $"{methodName} - Starting transaction for updating role to {newRoleName} for user with ID {userId}");

        try
        {
            return await _transactionService.ExecuteInTransactionAsync(async () =>
            {
                var currentUserRole = _authContext.Role() ??
                                      throw new InvalidOperationException("Current user's role is not defined.");

                _logger.LogDebug($"{methodName} - Current user role: {currentUserRole}");

                if (!Roles.RoleLevels.TryGetValue(currentUserRole, out var currentUserRoleLevel))
                    throw new InvalidOperationException($"Role {currentUserRole} not found in the role levels dictionary.");

                var targetRole = await _roleManager.FindByNameAsync(newRoleName) ??
                                 throw new InvalidOperationException(
                                     $"It's impossible to change the role to a {newRoleName}, there is no such role");

                if (targetRole.Level > currentUserRoleLevel)
                    throw new InvalidOperationException(
                        $"Access denied. Trying to set a higher role {newRoleName} from current role {currentUserRole}.");

                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    throw new InvalidOperationException($"User with ID {userId} not found.");

                var currentRoles = await _userManager.GetRolesAsync(user);
                var removalResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!removalResult.Succeeded)
                    throw new InvalidOperationException(
                        $"Failed to remove current roles {string.Join(", ", currentRoles)} from user {userId}.");

                var addRoleResult = await _userManager.AddToRoleAsync(user, newRoleName);

                if (!addRoleResult.Succeeded)
                    throw new InvalidOperationException($"Failed to assign new role {newRoleName} to user {userId}.");

                _logger.LogInformation($"Role updated successfully to {newRoleName} for user {userId}");

                return ResponseBase.Success();
            }, cancellationToken: CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{methodName} - Error updating role for user {userId}: {ex.Message}");

            return ResponseBase.Fail(ex.Message);
        }
    }

}