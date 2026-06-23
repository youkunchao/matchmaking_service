using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<ChatMessage>> GetMessagesBetweenUsersAsync(Guid userId1, Guid userId2, int pageSize, int pageIndex)
    {
        return await _dbSet
            .Where(m => 
                (m.FromUserId == userId1 && m.ToUserId == userId2) ||
                (m.FromUserId == userId2 && m.ToUserId == userId1))
            .OrderByDescending(m => m.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(Guid userId)
    {
        return await _dbSet.Where(m => m.ToUserId == userId && !m.IsRead).CountAsync();
    }

    public async Task MarkMessagesAsReadAsync(Guid userId, Guid otherUserId)
    {
        var messages = await _dbSet
            .Where(m => m.ToUserId == userId && m.FromUserId == otherUserId && !m.IsRead)
            .ToListAsync();

        foreach (var message in messages)
        {
            message.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }
}