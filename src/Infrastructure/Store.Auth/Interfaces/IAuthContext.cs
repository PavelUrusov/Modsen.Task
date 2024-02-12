using Microsoft.AspNetCore.Http;

namespace Store.Auth.Interfaces;

public interface IAuthContext
{
    string? IpAddress();
    string? UserId();

    string? AccessToken();
    void AccessToken(string token, CookieOptions? options = null);
    string? RefreshToken();
    void RefreshToken(string token, CookieOptions? options = null);
    public void ResetRefreshToken();
    public void ResetAccessToken();
}