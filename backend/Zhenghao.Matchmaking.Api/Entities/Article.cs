using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("articles")]
public class Article
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Column("content")]
    public string? Content { get; set; }

    [Column("summary")]
    [MaxLength(500)]
    public string? Summary { get; set; }

    [Column("cover_image")]
    [MaxLength(500)]
    public string? CoverImage { get; set; }

    [Column("category")]
    [MaxLength(50)]
    public string? Category { get; set; }

    [Column("author")]
    [MaxLength(100)]
    public string? Author { get; set; }

    [Column("views")]
    public int Views { get; set; } = 0;

    [Column("likes")]
    public int Likes { get; set; } = 0;

    [Column("status")]
    public int Status { get; set; } = 0; // 0: draft, 1: published

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [Column("published_at")]
    public DateTime? PublishedAt { get; set; }
}