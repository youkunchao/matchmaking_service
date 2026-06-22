using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(Guid userId, OrderCreateRequest request);
    Task<OrderResponse> PayOrderAsync(OrderPayRequest request);
    Task<OrderResponse> CancelOrderAsync(Guid orderId, Guid userId);
    Task<OrderResponse> RefundOrderAsync(Guid orderId, Guid userId, string reason);
    Task<List<OrderResponse>> GetUserOrdersAsync(Guid userId);
    Task<OrderResponse> GetOrderByIdAsync(Guid orderId);
}