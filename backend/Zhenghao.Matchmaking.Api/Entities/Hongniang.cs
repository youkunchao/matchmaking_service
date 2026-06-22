using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("hongniang")]
public class Hongniang
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Column("password_hash")]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Column("avatar")]
    [MaxLength(255)]
    public string? Avatar { get; set; }

    [Column("phone")]
    [MaxLength(20)]
    public string? Phone { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("experience_years")]
    public int? ExperienceYears { get; set; }

    [Column("specialty")]
    [MaxLength(200)]
    public string? Specialty { get; set; }

    [Column("success_count")]
    public int SuccessCount { get; set; } = 0;

    [Column("service_philosophy")]
    [MaxLength(500)]
    public string? ServicePhilosophy { get; set; }

    [Column("status")]
    public int Status { get; set; } = 1;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}