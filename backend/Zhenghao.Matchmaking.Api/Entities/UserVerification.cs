using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("user_verifications")]
public class UserVerification
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("type")]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [Column("status")]
    public int Status { get; set; } = 0;

    [Column("verified_at")]
    public DateTime? VerifiedAt { get; set; }

    [Column("reject_reason")]
    [MaxLength(200)]
    public string? RejectReason { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}