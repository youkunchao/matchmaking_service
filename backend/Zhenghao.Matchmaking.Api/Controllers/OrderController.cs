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
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> Create([FromBody] OrderCreateRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _orderService.CreateOrderAsync(userId, request);
            return Ok(ApiResponse<OrderResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<OrderResponse>.Error(ex.Message));
        }
    }

    [HttpPost("pay")]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> Pay([FromBody] OrderPayRequest request)
    {
        try
        {
            var result = await _orderService.PayOrderAsync(request);
            return Ok(ApiResponse<OrderResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<OrderResponse>.Error(ex.Message));
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> Cancel(Guid id)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _orderService.CancelOrderAsync(id, userId);
            return Ok(ApiResponse<OrderResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<OrderResponse>.Error(ex.Message));
        }
    }

    [HttpPost("{id}/refund")]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> Refund(Guid id, [FromQuery] string reason)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _orderService.RefundOrderAsync(id, userId, reason);
            return Ok(ApiResponse<OrderResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<OrderResponse>.Error(ex.Message));
        }
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<OrderResponse>>>> GetMyOrders()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _orderService.GetUserOrdersAsync(userId);
            return Ok(ApiResponse<List<OrderResponse>>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<OrderResponse>>.Error(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> GetOrderById(Guid id)
    {
        try
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return Ok(ApiResponse<OrderResponse>.Success(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<OrderResponse>.Error(ex.Message));
        }
    }
}