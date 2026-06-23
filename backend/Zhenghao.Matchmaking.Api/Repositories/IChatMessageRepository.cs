using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<List<ChatMessage>> GetMessagesBetweenUsersAsync(Guid userId1, Guid userId2, int pageSize, int pageIndex);
    Task<int> GetUnreadCountAsync(Guid userId);
    Task MarkMessagesAsReadAsync(Guid userId, Guid otherUserId);
}