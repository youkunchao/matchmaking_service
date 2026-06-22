using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class ActivityCreateRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CoverImage { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    public decimal Fee { get; set; } = 0;

    public int MaxParticipants { get; set; } = 0;

    [MaxLength(500)]
    public string? Requirements { get; set; }
}

public class ActivityUpdateRequest
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? CoverImage { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [MaxLength(200)]
    public string? Location { get; set; }

    public decimal? Fee { get; set; }

    public int? MaxParticipants { get; set; }

    [MaxLength(500)]
    public string? Requirements { get; set; }

    public int? Status { get; set; }
}