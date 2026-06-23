using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("member_packages")]
public class MemberPackage
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("duration_months")]
    public int DurationMonths { get; set; } = 0;

    [Column("price")]
    public decimal Price { get; set; } = 0;

    [Column("description")]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Column("features")]
    [MaxLength(1000)]
    public string? Features { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; } = 0;

    [Column("status")]
    public int Status { get; set; } = 1;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}