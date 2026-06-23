using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class HongniangResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int? ExperienceYears { get; set; }
    public string? Specialty { get; set; }
    public int SuccessCount { get; set; }
    public string? ServicePhilosophy { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static HongniangResponse FromEntity(Hongniang hongniang)
    {
        return new HongniangResponse
        {
            Id = hongniang.Id,
            Username = hongniang.Username,
            Name = hongniang.Name,
            Avatar = hongniang.Avatar,
            Phone = hongniang.Phone,
            Email = hongniang.Email,
            ExperienceYears = hongniang.ExperienceYears,
            Specialty = hongniang.Specialty,
            SuccessCount = hongniang.SuccessCount,
            ServicePhilosophy = hongniang.ServicePhilosophy,
            Status = hongniang.Status,
            CreatedAt = hongniang.CreatedAt,
            UpdatedAt = hongniang.UpdatedAt
        };
    }
}