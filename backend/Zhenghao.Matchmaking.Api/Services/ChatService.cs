using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class ChatService : IChatService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IMatchRepository _matchRepository;

    public ChatService(IChatMessageRepository chatMessageRepository, IMatchRepository matchRepository)
    {
        _chatMessageRepository = chatMessageRepository;
        _matchRepository = matchRepository;
    }

    public async Task<ChatMessageResponse> SendMessageAsync(Guid senderId, ChatMessageRequest request)
    {
        var isMutual = await _matchRepository.GetMatchBetweenUsersAsync(senderId, request.TargetUserId);
        
        if (isMutual == null || !isMutual.IsMutual)
        {
            throw new Exception("需要双方互感兴趣才能聊天");
        }

        var message = new ChatMessage
        {
            FromUserId = senderId,
            ToUserId = request.TargetUserId,
            Content = request.Content,
            Type = request.MessageType,
            CreatedAt = DateTime.Now,
            IsRead = false
        };

        var createdMessage = await _chatMessageRepository.AddAsync(message);
        return ChatMessageResponse.FromEntity(createdMessage);
    }

    public async Task<List<ChatMessageResponse>> GetMessagesAsync(Guid userId, Guid otherUserId, int pageSize, int pageIndex)
    {
        var messages = await _chatMessageRepository.GetMessagesBetweenUsersAsync(userId, otherUserId, pageSize, pageIndex);
        return messages.Select(ChatMessageResponse.FromEntity).ToList();
    }

    public async Task<int> GetUnreadCountAsync(Guid userId)
    {
        return await _chatMessageRepository.GetUnreadCountAsync(userId);
    }

    public async Task MarkMessagesAsReadAsync(Guid userId, Guid otherUserId)
    {
        await _chatMessageRepository.MarkMessagesAsReadAsync(userId, otherUserId);
    }
}