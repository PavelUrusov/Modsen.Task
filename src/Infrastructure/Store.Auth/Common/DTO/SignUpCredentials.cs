namespace Store.Auth.Common.DTO;

public record SignUpCredentials
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}