using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("user_photos")]
public class UserPhoto
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("url")]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    [Column("is_main")]
    public bool IsMain { get; set; } = false;

    [Column("status")]
    public int Status { get; set; } = 1;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}