using System.Security.Claims;
using Store.Auth.Interfaces;

namespace Store.WebApi.Common;

public class AuthContext : IAuthContext
{

    private const string AccessTokenCookieName = "X-Access-Token";
    private const string RefreshTokenCookieName = "X-Refresh-Token";
    private readonly HttpContext _httpContext;

    public AuthContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException("Something went wrong...");
    }

    public string? IpAddress()
    {
        return _httpContext.Connection.RemoteIpAddress?.ToString();
    }

    public string? UserId()
    {
        return _httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public void ResetRefreshToken()
    {
        _httpContext.Response.Cookies.Delete(RefreshTokenCookieName);
    }

    public void ResetAccessToken()
    {
        _httpContext.Response.Cookies.Delete(AccessTokenCookieName);
    }

    public string? AccessToken()
    {
        return _httpContext.Request.Cookies[AccessTokenCookieName];
    }

    public void AccessToken(string token, CookieOptions? options = null)
    {
        AppendCookie(AccessTokenCookieName, token, options);
    }

    public void RefreshToken(string token, CookieOptions? options = null)
    {
        AppendCookie(RefreshTokenCookieName, token, options);
    }

    public string? RefreshToken()
    {
        return _httpContext.Request.Cookies[RefreshTokenCookieName];
    }

    private void AppendCookie(string key, string value, CookieOptions? options = null)
    {
        if (options == null)
        {
            _httpContext.Response.Cookies.Append(key, value);

            return;
        }

        _httpContext.Response.Cookies.Append(key, value, options);
    }

}