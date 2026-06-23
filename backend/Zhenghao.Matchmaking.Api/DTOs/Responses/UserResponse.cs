using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? OpenId { get; set; }
    public string? Phone { get; set; }
    public string? Nickname { get; set; }
    public string? Avatar { get; set; }
    public int? Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public string? Education { get; set; }
    public string? Occupation { get; set; }
    public string? Income { get; set; }
    public string? MaritalStatus { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Address { get; set; }
    public string? LivingStatus { get; set; }
    public string? Smoking { get; set; }
    public string? Drinking { get; set; }
    public string? ChildrenWish { get; set; }
    public string? MarriagePlan { get; set; }
    public string? SelfIntro { get; set; }
    public string? HobbyTags { get; set; }
    public string? CharacterTags { get; set; }
    public string? LoveViewTags { get; set; }
    public string MemberLevel { get; set; } = string.Empty;
    public DateTime? MemberExpireTime { get; set; }
    public bool IsRealNameVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static UserResponse FromEntity(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            OpenId = user.OpenId,
            Phone = user.Phone,
            Nickname = user.Nickname,
            Avatar = user.Avatar,
            Gender = user.Gender,
            Birthday = user.Birthday,
            Height = user.Height,
            Weight = user.Weight,
            Education = user.Education,
            Occupation = user.Occupation,
            Income = user.Income,
            MaritalStatus = user.MaritalStatus,
            City = user.City,
            District = user.District,
            Address = user.Address,
            LivingStatus = user.LivingStatus,
            Smoking = user.Smoking,
            Drinking = user.Drinking,
            ChildrenWish = user.ChildrenWish,
            MarriagePlan = user.MarriagePlan,
            SelfIntro = user.SelfIntro,
            HobbyTags = user.HobbyTags,
            CharacterTags = user.CharacterTags,
            LoveViewTags = user.LoveViewTags,
            MemberLevel = user.MemberLevel,
            MemberExpireTime = user.MemberExpireTime,
            IsRealNameVerified = user.IsRealNameVerified,
            VerifiedAt = user.VerifiedAt,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}