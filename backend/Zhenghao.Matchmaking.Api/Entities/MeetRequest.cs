using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("meet_requests")]
public class MeetRequest
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("target_user_id")]
    public Guid TargetUserId { get; set; }

    [Column("status")]
    public int Status { get; set; } = 0;

    [Column("meet_time")]
    public DateTime? MeetTime { get; set; }

    [Column("location")]
    [MaxLength(200)]
    public string? Location { get; set; }

    [Column("remark")]
    [MaxLength(500)]
    public string? Remark { get; set; }

    [Column("hongniang_id")]
    public Guid? HongniangId { get; set; }

    [Column("feedback")]
    [MaxLength(500)]
    public string? Feedback { get; set; }

    [Column("feedback_score")]
    public int? FeedbackScore { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("TargetUserId")]
    public virtual User TargetUser { get; set; } = null!;

    [ForeignKey("HongniangId")]
    public virtual Hongniang? Hongniang { get; set; }
}