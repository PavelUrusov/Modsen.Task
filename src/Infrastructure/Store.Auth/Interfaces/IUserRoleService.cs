using Store.Application.Common;
using Store.Auth.Common.DTO;

namespace Store.Auth.Interfaces;

public interface IUserRoleService
{

    Task<ResponseBase> UpdateUserRoleAsync(UpdateUserRoleCredentials credentials);

}