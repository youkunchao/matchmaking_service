using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class UserRegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string OpenId { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? UnionId { get; set; }

    [MaxLength(20)]
    [Phone]
    public string? Phone { get; set; }

    [MaxLength(50)]
    public string? Nickname { get; set; }

    [MaxLength(255)]
    public string? Avatar { get; set; }

    public int? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    [MaxLength(50)]
    public string? Education { get; set; }

    [MaxLength(100)]
    public string? Occupation { get; set; }

    [MaxLength(50)]
    public string? Income { get; set; }

    [MaxLength(20)]
    public string? MaritalStatus { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? District { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }
}