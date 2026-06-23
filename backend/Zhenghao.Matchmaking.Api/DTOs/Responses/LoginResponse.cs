namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; }
    public UserResponse User { get; set; } = null!;
}