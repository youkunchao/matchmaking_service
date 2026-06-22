using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<ActivityResponse> CreateActivityAsync(ActivityCreateRequest request)
    {
        var activity = new Activity
        {
            Title = request.Title,
            CoverImage = request.CoverImage,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Fee = request.Fee,
            MaxParticipants = request.MaxParticipants,
            Requirements = request.Requirements,
            Status = 0
        };

        var createdActivity = await _activityRepository.AddAsync(activity);
        return ActivityResponse.FromEntity(createdActivity);
    }

    public async Task<ActivityResponse> UpdateActivityAsync(Guid id, ActivityUpdateRequest request)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
        {
            throw new Exception("活动不存在");
        }

        if (request.Title != null) activity.Title = request.Title;
        if (request.CoverImage != null) activity.CoverImage = request.CoverImage;
        if (request.Description != null) activity.Description = request.Description;
        if (request.StartTime.HasValue) activity.StartTime = request.StartTime.Value;
        if (request.EndTime.HasValue) activity.EndTime = request.EndTime.Value;
        if (request.Location != null) activity.Location = request.Location;
        if (request.Fee.HasValue) activity.Fee = request.Fee.Value;
        if (request.MaxParticipants.HasValue) activity.MaxParticipants = request.MaxParticipants.Value;
        if (request.Requirements != null) activity.Requirements = request.Requirements;
        if (request.Status.HasValue) activity.Status = request.Status.Value;

        activity.UpdatedAt = DateTime.Now;

        var updatedActivity = await _activityRepository.UpdateAsync(activity);
        return ActivityResponse.FromEntity(updatedActivity);
    }

    public async Task DeleteActivityAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
        {
            throw new Exception("活动不存在");
        }
        await _activityRepository.DeleteAsync(id);
    }

    public async Task<ActivityResponse> GetActivityByIdAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
        {
            throw new Exception("活动不存在");
        }
        return ActivityResponse.FromEntity(activity);
    }

    public async Task<List<ActivityResponse>> GetActivitiesAsync(int status)
    {
        var activities = await _activityRepository.GetByStatusAsync(status);
        return activities.Select(ActivityResponse.FromEntity).ToList();
    }

    public async Task<List<ActivityResponse>> GetUpcomingActivitiesAsync()
    {
        var activities = await _activityRepository.GetUpcomingActivitiesAsync();
        return activities.Select(ActivityResponse.FromEntity).ToList();
    }
}