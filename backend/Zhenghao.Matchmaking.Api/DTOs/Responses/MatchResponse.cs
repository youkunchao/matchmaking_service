using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class MatchRecordResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TargetUserId { get; set; }
    public UserResponse? TargetUser { get; set; }
    public decimal MatchScore { get; set; }
    public string? MatchReason { get; set; }
    public bool IsMutual { get; set; }
    public int UserInterest { get; set; }
    public int TargetInterest { get; set; }
    public string MatchType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public static MatchRecordResponse FromEntity(MatchRecord record, User? targetUser = null)
    {
        return new MatchRecordResponse
        {
            Id = record.Id,
            UserId = record.UserId,
            TargetUserId = record.TargetUserId,
            TargetUser = targetUser != null ? UserResponse.FromEntity(targetUser) : null,
            MatchScore = record.MatchScore,
            MatchReason = record.MatchReason,
            IsMutual = record.IsMutual,
            UserInterest = record.UserInterest,
            TargetInterest = record.TargetInterest,
            MatchType = record.MatchType,
            CreatedAt = record.CreatedAt
        };
    }
}

public class MeetRequestResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TargetUserId { get; set; }
    public UserResponse? TargetUser { get; set; }
    public int Status { get; set; }
    public string? StatusText { get; set; }
    public DateTime? MeetTime { get; set; }
    public string? Location { get; set; }
    public string? Remark { get; set; }
    public string? Feedback { get; set; }
    public int? FeedbackScore { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static MeetRequestResponse FromEntity(MeetRequest request, User? targetUser = null)
    {
        return new MeetRequestResponse
        {
            Id = request.Id,
            UserId = request.UserId,
            TargetUserId = request.TargetUserId,
            TargetUser = targetUser != null ? UserResponse.FromEntity(targetUser) : null,
            Status = request.Status,
            StatusText = request.Status switch
            {
                0 => "待确认",
                1 => "已确认",
                2 => "已完成",
                3 => "已取消",
                _ => "未知"
            },
            MeetTime = request.MeetTime,
            Location = request.Location,
            Remark = request.Remark,
            Feedback = request.Feedback,
            FeedbackScore = request.FeedbackScore,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt
        };
    }
}