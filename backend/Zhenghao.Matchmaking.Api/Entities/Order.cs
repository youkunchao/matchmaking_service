using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("order_no")]
    [MaxLength(50)]
    public string OrderNo { get; set; } = string.Empty;

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("type")]
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;

    [Column("package_id")]
    public Guid? PackageId { get; set; }

    [Column("activity_id")]
    public Guid? ActivityId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; } = 0;

    [Column("status")]
    [MaxLength(20)]
    public string Status { get; set; } = "待支付";

    [Column("payment_time")]
    public DateTime? PaymentTime { get; set; }

    [Column("refund_amount")]
    public decimal RefundAmount { get; set; } = 0;

    [Column("refund_time")]
    public DateTime? RefundTime { get; set; }

    [Column("refund_reason")]
    [MaxLength(500)]
    public string? RefundReason { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("PackageId")]
    public virtual MemberPackage? Package { get; set; }

    [ForeignKey("ActivityId")]
    public virtual Activity? Activity { get; set; }
}