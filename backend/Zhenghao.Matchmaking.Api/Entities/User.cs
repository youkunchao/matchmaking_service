using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhenghao.Matchmaking.Api.Entities;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("open_id")]
    [MaxLength(100)]
    public string? OpenId { get; set; }

    [Column("union_id")]
    [MaxLength(100)]
    public string? UnionId { get; set; }

    [Column("phone")]
    [MaxLength(20)]
    public string? Phone { get; set; }

    [Column("nickname")]
    [MaxLength(50)]
    public string? Nickname { get; set; }

    [Column("avatar")]
    [MaxLength(255)]
    public string? Avatar { get; set; }

    [Column("gender")]
    public int? Gender { get; set; }

    [Column("birthday")]
    public DateOnly? Birthday { get; set; }

    [Column("height")]
    public int? Height { get; set; }

    [Column("weight")]
    public int? Weight { get; set; }

    [Column("education")]
    [MaxLength(50)]
    public string? Education { get; set; }

    [Column("occupation")]
    [MaxLength(100)]
    public string? Occupation { get; set; }

    [Column("income")]
    [MaxLength(50)]
    public string? Income { get; set; }

    [Column("marital_status")]
    [MaxLength(20)]
    public string? MaritalStatus { get; set; }

    [Column("city")]
    [MaxLength(50)]
    public string? City { get; set; }

    [Column("district")]
    [MaxLength(50)]
    public string? District { get; set; }

    [Column("address")]
    [MaxLength(255)]
    public string? Address { get; set; }

    [Column("living_status")]
    [MaxLength(50)]
    public string? LivingStatus { get; set; }

    [Column("smoking")]
    [MaxLength(20)]
    public string? Smoking { get; set; }

    [Column("drinking")]
    [MaxLength(20)]
    public string? Drinking { get; set; }

    [Column("children_wish")]
    [MaxLength(50)]
    public string? ChildrenWish { get; set; }

    [Column("marriage_plan")]
    [MaxLength(200)]
    public string? MarriagePlan { get; set; }

    [Column("self_intro")]
    [MaxLength(1000)]
    public string? SelfIntro { get; set; }

    [Column("hobby_tags")]
    [MaxLength(500)]
    public string? HobbyTags { get; set; }

    [Column("character_tags")]
    [MaxLength(500)]
    public string? CharacterTags { get; set; }

    [Column("love_view_tags")]
    [MaxLength(500)]
    public string? LoveViewTags { get; set; }

    [Column("member_level")]
    [MaxLength(50)]
    public string MemberLevel { get; set; } = "普通会员";

    [Column("member_expire_time")]
    public DateTime? MemberExpireTime { get; set; }

    [Column("hongniang_id")]
    public Guid? HongniangId { get; set; }

    [Column("is_real_name_verified")]
    public bool IsRealNameVerified { get; set; } = false;

    [Column("verified_at")]
    public DateTime? VerifiedAt { get; set; }

    [Column("status")]
    public int Status { get; set; } = 1;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("HongniangId")]
    public virtual Hongniang? Hongniang { get; set; }

    public virtual ICollection<UserPhoto> Photos { get; set; } = new List<UserPhoto>();
    public virtual ICollection<UserVerification> Verifications { get; set; } = new List<UserVerification>();
    public virtual ICollection<MatchRecord> MatchRecords { get; set; } = new List<MatchRecord>();
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    public virtual ICollection<MeetRequest> MeetRequests { get; set; } = new List<MeetRequest>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<ActivityParticipant> ActivityParticipants { get; set; } = new List<ActivityParticipant>();
}