using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

/// <summary>
/// 活动实体类
/// </summary>
[Table("activities")]
public class Activity
{
    /// <summary>
    /// 活动ID
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// 活动标题
    /// </summary>
    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 活动封面图
    /// </summary>
    [Column("cover_image")]
    [MaxLength(500)]
    public string? CoverImage { get; set; }

    /// <summary>
    /// 活动描述
    /// </summary>
    [Column("description")]
    [MaxLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// 活动开始时间
    /// </summary>
    [Column("start_time")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 活动结束时间
    /// </summary>
    [Column("end_time")]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 活动地点
    /// </summary>
    [Column("location")]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// 活动费用
    /// </summary>
    [Column("fee")]
    public decimal Fee { get; set; } = 0;

    /// <summary>
    /// 最大报名人数
    /// </summary>
    [Column("max_participants")]
    public int MaxParticipants { get; set; } = 0;

    /// <summary>
    /// 当前报名人数
    /// </summary>
    [Column("current_participants")]
    public int CurrentParticipants { get; set; } = 0;

    /// <summary>
    /// 报名要求
    /// </summary>
    [Column("requirements")]
    [MaxLength(500)]
    public string? Requirements { get; set; }

    /// <summary>
    /// 活动状态：0-预告中 1-进行中 2-已结束
    /// </summary>
    [Column("status")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新时间
    /// </summary>
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 活动参与者列表
    /// </summary>
    public virtual ICollection<ActivityParticipant> Participants { get; set; } = new List<ActivityParticipant>();
}