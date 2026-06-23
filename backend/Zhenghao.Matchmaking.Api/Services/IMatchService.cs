using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IMatchService
{
    Task<List<MatchRecordResponse>> GetUserMatchesAsync(Guid userId);
    Task<List<MatchRecordResponse>> GetMutualMatchesAsync(Guid userId);
    Task<MatchRecordResponse> LikeUserAsync(Guid userId, MatchRequestDto request);
    Task<MatchRecordResponse?> GetMatchBetweenUsersAsync(Guid userId, Guid targetUserId);
    Task<List<MatchRecordResponse>> GetRecommendedMatchesAsync(Guid userId, int count);
}