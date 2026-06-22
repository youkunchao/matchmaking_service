using System.ComponentModel.DataAnnotations;

namespace Zhenghao.Matchmaking.Api.DTOs.Requests;

public class OrderCreateRequest
{
    [Required]
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;

    public Guid? PackageId { get; set; }

    public Guid? ActivityId { get; set; }

    public decimal Amount { get; set; } = 0;
}

public class OrderPayRequest
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;
}