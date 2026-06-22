using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HongniangController : ControllerBase
{
    private readonly IHongniangService _hongniangService;

    public HongniangController(IHongniangService hongniangService)
    {
        _hongniangService = hongniangService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<HongniangResponse>>>> GetActiveHongniang()
    {
        try
        {
            var result = await _hongniangService.GetActiveHongniangAsync();
            return Ok(ApiResponse<List<HongniangResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<HongniangResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<HongniangResponse>>> GetById(Guid id)
    {
        try
        {
            var result = await _hongniangService.GetHongniangByIdAsync(id);
            return Ok(ApiResponse<HongniangResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<HongniangResponse>.Error(ex.Message));
        }
    }

    [HttpGet("{id}/users")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<UserResponse>>>> GetHongniangUsers(Guid id)
    {
        try
        {
            var result = await _hongniangService.GetHongniangUsersAsync(id);
            return Ok(ApiResponse<List<UserResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<UserResponse>>.Error(ex.Message));
        }
    }
}