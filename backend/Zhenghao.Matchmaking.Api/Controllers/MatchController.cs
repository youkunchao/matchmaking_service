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
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<MatchRecordResponse>>>> GetMyMatches()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _matchService.GetUserMatchesAsync(userId);
            return Ok(ApiResponse<List<MatchRecordResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<MatchRecordResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("mutual")]
    public async Task<ActionResult<ApiResponse<List<MatchRecordResponse>>>> GetMutualMatches()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _matchService.GetMutualMatchesAsync(userId);
            return Ok(ApiResponse<List<MatchRecordResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<MatchRecordResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("recommend")]
    public async Task<ActionResult<ApiResponse<List<MatchRecordResponse>>>> GetRecommendations([FromQuery] int count = 8)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _matchService.GetRecommendedMatchesAsync(userId, count);
            return Ok(ApiResponse<List<MatchRecordResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<MatchRecordResponse>>.Error(ex.Message));
        }
    }

    [HttpPost("like")]
    public async Task<ActionResult<ApiResponse<MatchRecordResponse>>> LikeUser([FromBody] MatchRequestDto request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _matchService.LikeUserAsync(userId, request);
            return Ok(ApiResponse<MatchRecordResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MatchRecordResponse>.Error(ex.Message));
        }
    }

    [HttpGet("check/{targetUserId}")]
    public async Task<ActionResult<ApiResponse<MatchRecordResponse?>>> CheckMatch(Guid targetUserId)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _matchService.GetMatchBetweenUsersAsync(userId, targetUserId);
            return Ok(ApiResponse<MatchRecordResponse?>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<MatchRecordResponse?>.Error(ex.Message));
        }
    }
}