using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class MatchRepository : GenericRepository<MatchRecord>, IMatchRepository
{
    public MatchRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<MatchRecord>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(m => m.UserId == userId).ToListAsync();
    }

    public async Task<List<MatchRecord>> GetMutualMatchesAsync(Guid userId)
    {
        return await _dbSet.Where(m => m.UserId == userId && m.IsMutual).ToListAsync();
    }

    public async Task<MatchRecord?> GetMatchBetweenUsersAsync(Guid userId, Guid targetUserId)
    {
        return await _dbSet.FirstOrDefaultAsync(m => 
            (m.UserId == userId && m.TargetUserId == targetUserId) ||
            (m.UserId == targetUserId && m.TargetUserId == userId));
    }

    public async Task<List<MatchRecord>> GetRecommendedMatchesAsync(Guid userId, int count)
    {
        return await _dbSet
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.MatchScore)
            .Take(count)
            .ToListAsync();
    }
}