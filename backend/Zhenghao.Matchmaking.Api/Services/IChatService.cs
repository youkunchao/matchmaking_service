using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IChatService
{
    Task<ChatMessageResponse> SendMessageAsync(Guid senderId, ChatMessageRequest request);
    Task<List<ChatMessageResponse>> GetMessagesAsync(Guid userId, Guid otherUserId, int pageSize, int pageIndex);
    Task<int> GetUnreadCountAsync(Guid userId);
    Task MarkMessagesAsReadAsync(Guid userId, Guid otherUserId);
}