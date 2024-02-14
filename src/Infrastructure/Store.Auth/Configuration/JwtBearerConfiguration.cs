namespace Store.Auth.Configuration;

public class JwtBearerConfiguration
{

    public string SecretKey { get; set; } = null!;
    public string AuthenticationScheme { get; set; } = null!;
    public bool RequireHttpsMetadata { get; set; }
    public bool SaveToken { get; set; }
    public bool ValidateIssuer { get; set; }
    public string? ValidIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public string? ValidAudience { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateLifetime { get; set; }
    public TimeSpan ClockSkew { get; set; }
    public TimeSpan AccessTokenExpiryTime { get; set; }
    public TimeSpan RefreshTokenExpiryTime { get; set; }

}