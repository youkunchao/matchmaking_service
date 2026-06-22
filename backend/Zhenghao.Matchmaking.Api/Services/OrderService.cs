using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMemberPackageRepository _memberPackageRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IMemberPackageRepository memberPackageRepository, IActivityRepository activityRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _memberPackageRepository = memberPackageRepository;
        _activityRepository = activityRepository;
        _userRepository = userRepository;
    }

    public async Task<OrderResponse> CreateOrderAsync(Guid userId, OrderCreateRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("用户不存在");
        }

        decimal amount = request.Amount;

        if (request.Type == "package" && request.PackageId.HasValue)
        {
            var package = await _memberPackageRepository.GetByIdAsync(request.PackageId.Value);
            if (package == null)
            {
                throw new Exception("套餐不存在");
            }
            amount = package.Price;
        }
        else if (request.Type == "activity" && request.ActivityId.HasValue)
        {
            var activity = await _activityRepository.GetByIdAsync(request.ActivityId.Value);
            if (activity == null)
            {
                throw new Exception("活动不存在");
            }
            amount = activity.Fee;

            if (await _activityRepository.HasParticipatedAsync(request.ActivityId.Value, userId))
            {
                throw new Exception("已报名该活动");
            }
        }

        var order = new Order
        {
            OrderNo = GenerateOrderNo(),
            UserId = userId,
            Type = request.Type,
            PackageId = request.PackageId,
            ActivityId = request.ActivityId,
            Amount = amount
        };

        var createdOrder = await _orderRepository.AddAsync(order);
        return OrderResponse.FromEntity(createdOrder);
    }

    public async Task<OrderResponse> PayOrderAsync(OrderPayRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new Exception("订单不存在");
        }

        if (order.Status != "待支付")
        {
            throw new Exception("订单状态不允许支付");
        }

        order.Status = "已支付";
        order.PaymentTime = DateTime.Now;

        var updatedOrder = await _orderRepository.UpdateAsync(order);

        if (order.Type == "package" && order.PackageId.HasValue)
        {
            await ProcessPackagePayment(order);
        }
        else if (order.Type == "activity" && order.ActivityId.HasValue)
        {
            await ProcessActivityPayment(order);
        }

        return OrderResponse.FromEntity(updatedOrder);
    }

    public async Task<OrderResponse> CancelOrderAsync(Guid orderId, Guid userId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("订单不存在");
        }

        if (order.UserId != userId)
        {
            throw new Exception("只能取消自己的订单");
        }

        if (order.Status != "待支付")
        {
            throw new Exception("订单状态不允许取消");
        }

        order.Status = "已取消";
        var updatedOrder = await _orderRepository.UpdateAsync(order);
        return OrderResponse.FromEntity(updatedOrder);
    }

    public async Task<OrderResponse> RefundOrderAsync(Guid orderId, Guid userId, string reason)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("订单不存在");
        }

        if (order.UserId != userId)
        {
            throw new Exception("只能退款自己的订单");
        }

        if (order.Status != "已支付")
        {
            throw new Exception("订单状态不允许退款");
        }

        order.Status = "已退款";
        order.RefundAmount = order.Amount;
        order.RefundTime = DateTime.Now;
        order.RefundReason = reason;

        var updatedOrder = await _orderRepository.UpdateAsync(order);
        return OrderResponse.FromEntity(updatedOrder);
    }

    public async Task<List<OrderResponse>> GetUserOrdersAsync(Guid userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);
        return orders.Select(OrderResponse.FromEntity).ToList();
    }

    public async Task<OrderResponse> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("订单不存在");
        }
        return OrderResponse.FromEntity(order);
    }

    private string GenerateOrderNo()
    {
        return $"ORD{DateTime.Now:yyyyMMddHHmmss}{Guid.NewGuid().ToString("N").Substring(0, 6)}";
    }

    private async Task ProcessPackagePayment(Order order)
    {
        var user = await _userRepository.GetByIdAsync(order.UserId);
        var package = await _memberPackageRepository.GetByIdAsync(order.PackageId.Value);

        if (user != null && package != null)
        {
            user.MemberLevel = package.Name;
            user.MemberExpireTime = DateTime.Now.AddMonths(package.DurationMonths);
            await _userRepository.UpdateAsync(user);
        }
    }

    private async Task ProcessActivityPayment(Order order)
    {
        var activity = await _activityRepository.GetByIdAsync(order.ActivityId.Value);
        if (activity != null && activity.CurrentParticipants < activity.MaxParticipants)
        {
            activity.CurrentParticipants++;
            await _activityRepository.UpdateAsync(activity);
        }
    }
}