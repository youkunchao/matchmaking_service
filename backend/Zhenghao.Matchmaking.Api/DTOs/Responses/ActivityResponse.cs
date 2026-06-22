using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class ActivityResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal Fee { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public string? Requirements { get; set; }
    public int Status { get; set; }
    public string? StatusText { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static ActivityResponse FromEntity(Activity activity)
    {
        return new ActivityResponse
        {
            Id = activity.Id,
            Title = activity.Title,
            CoverImage = activity.CoverImage,
            Description = activity.Description,
            StartTime = activity.StartTime,
            EndTime = activity.EndTime,
            Location = activity.Location,
            Fee = activity.Fee,
            MaxParticipants = activity.MaxParticipants,
            CurrentParticipants = activity.CurrentParticipants,
            Requirements = activity.Requirements,
            Status = activity.Status,
            StatusText = activity.Status switch
            {
                0 => "预告中",
                1 => "进行中",
                2 => "已结束",
                _ => "未知"
            },
            CreatedAt = activity.CreatedAt,
            UpdatedAt = activity.UpdatedAt
        };
    }
}