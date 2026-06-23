using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("chat_messages")]
public class ChatMessage
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("from_user_id")]
    public Guid FromUserId { get; set; }

    [Column("to_user_id")]
    public Guid ToUserId { get; set; }

    [Column("content")]
    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    [Column("type")]
    [MaxLength(20)]
    public string Type { get; set; } = "text";

    [Column("is_read")]
    public bool IsRead { get; set; } = false;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("FromUserId")]
    public virtual User FromUser { get; set; } = null!;

    [ForeignKey("ToUserId")]
    public virtual User ToUser { get; set; } = null!;
}