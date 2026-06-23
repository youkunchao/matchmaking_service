using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class MatchRequestDto
{
    [Required]
    public Guid TargetUserId { get; set; }

    public int Interest { get; set; } = 1;
}

public class MeetRequestDto
{
    [Required]
    public Guid TargetUserId { get; set; }

    public DateTime? MeetTime { get; set; }

    [MaxLength(200)]
    public string? Location { get; set; }

    [MaxLength(500)]
    public string? Remark { get; set; }
}

public class FeedbackRequest
{
    [Required]
    public Guid MeetRequestId { get; set; }

    [MaxLength(500)]
    public string? Feedback { get; set; }

    [Range(1, 5)]
    public int? FeedbackScore { get; set; }
}