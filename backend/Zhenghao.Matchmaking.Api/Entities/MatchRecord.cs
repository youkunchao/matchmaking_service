using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("match_records")]
public class MatchRecord
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("target_user_id")]
    public Guid TargetUserId { get; set; }

    [Column("match_score")]
    public decimal MatchScore { get; set; } = 0;

    [Column("match_reason")]
    [MaxLength(500)]
    public string? MatchReason { get; set; }

    [Column("is_mutual")]
    public bool IsMutual { get; set; } = false;

    [Column("user_interest")]
    public int UserInterest { get; set; } = 0;

    [Column("target_interest")]
    public int TargetInterest { get; set; } = 0;

    [Column("match_type")]
    [MaxLength(20)]
    public string MatchType { get; set; } = "auto";

    [Column("hongniang_id")]
    public Guid? HongniangId { get; set; }

    [Column("hongniang_comment")]
    [MaxLength(500)]
    public string? HongniangComment { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("TargetUserId")]
    public virtual User TargetUser { get; set; } = null!;

    [ForeignKey("HongniangId")]
    public virtual Hongniang? Hongniang { get; set; }
}