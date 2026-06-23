using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IMatchRepository : IRepository<MatchRecord>
{
    Task<List<MatchRecord>> GetByUserIdAsync(Guid userId);
    Task<List<MatchRecord>> GetMutualMatchesAsync(Guid userId);
    Task<MatchRecord?> GetMatchBetweenUsersAsync(Guid userId, Guid targetUserId);
    Task<List<MatchRecord>> GetRecommendedMatchesAsync(Guid userId, int count);
}