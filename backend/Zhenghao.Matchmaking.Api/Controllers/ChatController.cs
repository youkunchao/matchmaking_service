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
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("send")]
    public async Task<ActionResult<ApiResponse<ChatMessageResponse>>> SendMessage([FromBody] ChatMessageRequest request)
    {
        try
        {
            var senderId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _chatService.SendMessageAsync(senderId, request);
            return Ok(ApiResponse<ChatMessageResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<ChatMessageResponse>.Error(ex.Message));
        }
    }

    [HttpGet("messages/{otherUserId}")]
    public async Task<ActionResult<ApiResponse<List<ChatMessageResponse>>>> GetMessages(Guid otherUserId, [FromQuery] int pageSize = 20, [FromQuery] int pageIndex = 0)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _chatService.GetMessagesAsync(userId, otherUserId, pageSize, pageIndex);
            return Ok(ApiResponse<List<ChatMessageResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<ChatMessageResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("unread")]
    public async Task<ActionResult<ApiResponse<int>>> GetUnreadCount()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _chatService.GetUnreadCountAsync(userId);
            return Ok(ApiResponse<int>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<int>.Error(ex.Message));
        }
    }

    [HttpPost("read/{otherUserId}")]
    public async Task<ActionResult<ApiResponse>> MarkAsRead(Guid otherUserId)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            await _chatService.MarkMessagesAsReadAsync(userId, otherUserId);
            return Ok(ApiResponse.Success());
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Error(ex.Message));
        }
    }
}