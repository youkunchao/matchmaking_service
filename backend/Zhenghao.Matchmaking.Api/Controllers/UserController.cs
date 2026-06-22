using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Register([FromBody] UserRegisterRequest request)
    {
        try
        {
            var result = await _userService.RegisterAsync(request);
            return Ok(ApiResponse<UserResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserResponse>.Error(ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _userService.LoginAsync(request);
            return Ok(ApiResponse<LoginResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<LoginResponse>.Error(ex.Message));
        }
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponse>>> GetProfile()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _userService.GetUserByIdAsync(userId);
            return Ok(ApiResponse<UserResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserResponse>.Error(ex.Message));
        }
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateProfile([FromBody] UserUpdateRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _userService.UpdateUserAsync(userId, request);
            return Ok(ApiResponse<UserResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserResponse>.Error(ex.Message));
        }
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<UserResponse>>>> Search([FromQuery] Dictionary<string, object> conditions)
    {
        try
        {
            var result = await _userService.SearchUsersAsync(conditions);
            return Ok(ApiResponse<List<UserResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<UserResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserById(Guid id)
    {
        try
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(ApiResponse<UserResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserResponse>.Error(ex.Message));
        }
    }
}