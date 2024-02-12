using Store.Application.Common;

namespace Store.Auth.DTO;

public record SignInResponse(string AccessToken, string RefreshToken) : ResponseBase;