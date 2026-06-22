using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class ChatMessageResponse
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public DateTime SendTime { get; set; }
    public bool IsRead { get; set; }

    public static ChatMessageResponse FromEntity(ChatMessage message)
    {
        return new ChatMessageResponse
        {
            Id = message.Id,
            SenderId = message.FromUserId,
            ReceiverId = message.ToUserId,
            Content = message.Content,
            MessageType = message.Type,
            SendTime = message.CreatedAt,
            IsRead = message.IsRead
        };
    }
}