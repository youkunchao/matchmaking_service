using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class LoginRequest
{
    [Required]
    [MaxLength(100)]
    public string OpenId { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? UnionId { get; set; }
}