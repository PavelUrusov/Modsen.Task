using Store.Application.Common;
using Store.Auth.DTO;

namespace Store.Auth.Interfaces;

public interface IAuthService
{
    public Task<ResponseBase> SignOutAsync();
    public Task<ResponseBase> RefreshTokenAsync();
    public Task<ResponseBase> SignUpAsync(SignUpCredentials credentials);
    public Task<ResponseBase> SignInAsync(SignInCredentials credentials);
}