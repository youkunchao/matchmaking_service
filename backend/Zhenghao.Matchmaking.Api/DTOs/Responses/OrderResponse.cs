using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.DTOs.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Type { get; set; } = string.Empty;
    public Guid? PackageId { get; set; }
    public Guid? ActivityId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? PaymentTime { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime? RefundTime { get; set; }
    public string? RefundReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static OrderResponse FromEntity(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            OrderNo = order.OrderNo,
            UserId = order.UserId,
            Type = order.Type,
            PackageId = order.PackageId,
            ActivityId = order.ActivityId,
            Amount = order.Amount,
            Status = order.Status,
            PaymentTime = order.PaymentTime,
            RefundAmount = order.RefundAmount,
            RefundTime = order.RefundTime,
            RefundReason = order.RefundReason,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }
}