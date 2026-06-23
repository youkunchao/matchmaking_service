using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IActivityService
{
    Task<ActivityResponse> CreateActivityAsync(ActivityCreateRequest request);
    Task<ActivityResponse> UpdateActivityAsync(Guid id, ActivityUpdateRequest request);
    Task DeleteActivityAsync(Guid id);
    Task<ActivityResponse> GetActivityByIdAsync(Guid id);
    Task<List<ActivityResponse>> GetActivitiesAsync(int status);
    Task<List<ActivityResponse>> GetUpcomingActivitiesAsync();
}