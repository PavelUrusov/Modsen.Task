namespace Store.Auth.DTO;

public record SignUpCredentials
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}