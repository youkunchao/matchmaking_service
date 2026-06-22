using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class UserUpdateRequest
{
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

    [MaxLength(50)]
    public string? LivingStatus { get; set; }

    [MaxLength(20)]
    public string? Smoking { get; set; }

    [MaxLength(20)]
    public string? Drinking { get; set; }

    [MaxLength(50)]
    public string? ChildrenWish { get; set; }

    [MaxLength(200)]
    public string? MarriagePlan { get; set; }

    [MaxLength(1000)]
    public string? SelfIntro { get; set; }

    [MaxLength(500)]
    public string? HobbyTags { get; set; }

    [MaxLength(500)]
    public string? CharacterTags { get; set; }

    [MaxLength(500)]
    public string? LoveViewTags { get; set; }
}