using FluentValidation;
using Store.Auth.Common.DTO;

namespace Store.WebApi.Common.Validation.RolesManagementController;

public class UpdateUserRoleValidation : AbstractValidator<UpdateUserRoleCredentials>
{

    public UpdateUserRoleValidation()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("Id is required");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role can't be empty");
    }
}