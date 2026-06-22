using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IUserRepository _userRepository;

    public MatchService(IMatchRepository matchRepository, IUserRepository userRepository)
    {
        _matchRepository = matchRepository;
        _userRepository = userRepository;
    }

    public async Task<List<MatchRecordResponse>> GetUserMatchesAsync(Guid userId)
    {
        var records = await _matchRepository.GetByUserIdAsync(userId);
        var responses = new List<MatchRecordResponse>();

        foreach (var record in records)
        {
            var targetUser = await _userRepository.GetByIdAsync(record.TargetUserId);
            responses.Add(MatchRecordResponse.FromEntity(record, targetUser));
        }

        return responses;
    }

    public async Task<List<MatchRecordResponse>> GetMutualMatchesAsync(Guid userId)
    {
        var records = await _matchRepository.GetMutualMatchesAsync(userId);
        var responses = new List<MatchRecordResponse>();

        foreach (var record in records)
        {
            var targetUser = await _userRepository.GetByIdAsync(record.TargetUserId);
            responses.Add(MatchRecordResponse.FromEntity(record, targetUser));
        }

        return responses;
    }

    public async Task<MatchRecordResponse> LikeUserAsync(Guid userId, MatchRequestDto request)
    {
        if (userId == request.TargetUserId)
        {
            throw new Exception("不能喜欢自己");
        }

        var existingMatch = await _matchRepository.GetMatchBetweenUsersAsync(userId, request.TargetUserId);

        if (existingMatch != null)
        {
            existingMatch.UserInterest = request.Interest;
            
            if (existingMatch.TargetInterest > 0)
            {
                existingMatch.IsMutual = true;
            }

            var updatedRecord = await _matchRepository.UpdateAsync(existingMatch);
            var targetUser = await _userRepository.GetByIdAsync(request.TargetUserId);
            return MatchRecordResponse.FromEntity(updatedRecord, targetUser);
        }

        var score = await CalculateMatchScore(userId, request.TargetUserId);
        var reasons = await GenerateMatchReasons(userId, request.TargetUserId);

        var newMatch = new MatchRecord
        {
            UserId = userId,
            TargetUserId = request.TargetUserId,
            MatchScore = score,
            MatchReason = reasons,
            UserInterest = request.Interest,
            MatchType = "auto"
        };

        var createdRecord = await _matchRepository.AddAsync(newMatch);
        var targetUserNew = await _userRepository.GetByIdAsync(request.TargetUserId);
        return MatchRecordResponse.FromEntity(createdRecord, targetUserNew);
    }

    public async Task<MatchRecordResponse?> GetMatchBetweenUsersAsync(Guid userId, Guid targetUserId)
    {
        var record = await _matchRepository.GetMatchBetweenUsersAsync(userId, targetUserId);
        if (record == null) return null;

        var targetUser = await _userRepository.GetByIdAsync(record.TargetUserId);
        return MatchRecordResponse.FromEntity(record, targetUser);
    }

    public async Task<List<MatchRecordResponse>> GetRecommendedMatchesAsync(Guid userId, int count)
    {
        var records = await _matchRepository.GetRecommendedMatchesAsync(userId, count);
        var responses = new List<MatchRecordResponse>();

        foreach (var record in records)
        {
            var targetUser = await _userRepository.GetByIdAsync(record.TargetUserId);
            responses.Add(MatchRecordResponse.FromEntity(record, targetUser));
        }

        return responses;
    }

    private async Task<decimal> CalculateMatchScore(Guid userId, Guid targetUserId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var targetUser = await _userRepository.GetByIdAsync(targetUserId);

        if (user == null || targetUser == null) return 0;

        decimal score = 50;

        if (user.City == targetUser.City) score += 15;
        if (user.Education == targetUser.Education) score += 10;
        if (user.MaritalStatus == targetUser.MaritalStatus) score += 10;
        if (user.AgeRangeMatches(targetUser)) score += 10;
        if (user.ChildrenWish == targetUser.ChildrenWish) score += 5;

        return Math.Min(score, 100);
    }

    private async Task<string> GenerateMatchReasons(Guid userId, Guid targetUserId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var targetUser = await _userRepository.GetByIdAsync(targetUserId);

        if (user == null || targetUser == null) return string.Empty;

        var reasons = new List<string>();

        if (user.City == targetUser.City) reasons.Add("同城");
        if (user.Education == targetUser.Education) reasons.Add("学历相当");
        if (user.MaritalStatus == targetUser.MaritalStatus) reasons.Add("婚况相同");
        if (user.AgeRangeMatches(targetUser)) reasons.Add("年龄匹配");
        if (user.ChildrenWish == targetUser.ChildrenWish) reasons.Add("生育观一致");

        return string.Join("，", reasons);
    }
}

public static class UserExtensions
{
    public static bool AgeRangeMatches(this User user, User target)
    {
        if (!user.Birthday.HasValue || !target.Birthday.HasValue) return false;

        var userAge = DateTime.Now.Year - user.Birthday.Value.Year;
        var targetAge = DateTime.Now.Year - target.Birthday.Value.Year;

        return Math.Abs(userAge - targetAge) <= 5;
    }
}