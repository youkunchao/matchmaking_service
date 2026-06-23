using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MeetRequestController : ControllerBase
{
    private readonly IMeetRequestService _meetRequestService;

    public MeetRequestController(IMeetRequestService meetRequestService)
    {
        _meetRequestService = meetRequestService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<MeetRequestResponse>>> Create([FromBody] MeetRequestDto request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.CreateMeetRequestAsync(userId, request);
            return Ok(ApiResponse<MeetRequestResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MeetRequestResponse>.Error(ex.Message));
        }
    }

    [HttpPost("{id}/confirm")]
    public async Task<ActionResult<ApiResponse<MeetRequestResponse>>> Confirm(Guid id)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.ConfirmMeetRequestAsync(id, userId);
            return Ok(ApiResponse<MeetRequestResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MeetRequestResponse>.Error(ex.Message));
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<ApiResponse<MeetRequestResponse>>> Cancel(Guid id)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.CancelMeetRequestAsync(id, userId);
            return Ok(ApiResponse<MeetRequestResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MeetRequestResponse>.Error(ex.Message));
        }
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult<ApiResponse<MeetRequestResponse>>> Complete(Guid id)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.CompleteMeetRequestAsync(id, userId);
            return Ok(ApiResponse<MeetRequestResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MeetRequestResponse>.Error(ex.Message));
        }
    }

    [HttpPost("feedback")]
    public async Task<ActionResult<ApiResponse<MeetRequestResponse>>> SubmitFeedback([FromBody] FeedbackRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.SubmitFeedbackAsync(request, userId);
            return Ok(ApiResponse<MeetRequestResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MeetRequestResponse>.Error(ex.Message));
        }
    }

    [HttpGet("sent")]
    public async Task<ActionResult<ApiResponse<List<MeetRequestResponse>>>> GetSentRequests()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.GetUserMeetRequestsAsync(userId);
            return Ok(ApiResponse<List<MeetRequestResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<MeetRequestResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("received")]
    public async Task<ActionResult<ApiResponse<List<MeetRequestResponse>>>> GetReceivedRequests()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _meetRequestService.GetReceivedMeetRequestsAsync(userId);
            return Ok(ApiResponse<List<MeetRequestResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<MeetRequestResponse>>.Error(ex.Message));
        }
    }
}