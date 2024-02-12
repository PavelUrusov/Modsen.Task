using Store.Application.Common;

namespace Store.Auth.DTO;

public record RefreshTokenResponse(string Token) : ResponseBase
{
}