using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<ActivityResponse>>> Create([FromBody] ActivityCreateRequest request)
    {
        try
        {
            var result = await _activityService.CreateActivityAsync(request);
            return Ok(ApiResponse<ActivityResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<ActivityResponse>.Error(ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<ActivityResponse>>> Update(Guid id, [FromBody] ActivityUpdateRequest request)
    {
        try
        {
            var result = await _activityService.UpdateActivityAsync(id, request);
            return Ok(ApiResponse<ActivityResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<ActivityResponse>.Error(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> Delete(Guid id)
    {
        try
        {
            await _activityService.DeleteActivityAsync(id);
            return Ok(ApiResponse.Success());
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Error(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ActivityResponse>>> GetById(Guid id)
    {
        try
        {
            var result = await _activityService.GetActivityByIdAsync(id);
            return Ok(ApiResponse<ActivityResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<ActivityResponse>.Error(ex.Message));
        }
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ActivityResponse>>>> GetActivities([FromQuery] int status = -1)
    {
        try
        {
            List<ActivityResponse> result;
            if (status >= 0)
            {
                result = await _activityService.GetActivitiesAsync(status);
            }
            else
            {
                result = await _activityService.GetUpcomingActivitiesAsync();
            }
            return Ok(ApiResponse<List<ActivityResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<ActivityResponse>>.Error(ex.Message));
        }
    }
}