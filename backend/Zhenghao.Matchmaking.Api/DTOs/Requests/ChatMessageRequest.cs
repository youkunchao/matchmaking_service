using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class ChatMessageRequest
{
    [Required]
    public Guid TargetUserId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    [MaxLength(20)]
    public string MessageType { get; set; } = "text";
}