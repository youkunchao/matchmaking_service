using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("activity_participants")]
public class ActivityParticipant
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("activity_id")]
    public Guid ActivityId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("status")]
    public int Status { get; set; } = 0;

    [Column("registered_at")]
    public DateTime RegisteredAt { get; set; } = DateTime.Now;

    [ForeignKey("ActivityId")]
    public virtual Activity Activity { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}