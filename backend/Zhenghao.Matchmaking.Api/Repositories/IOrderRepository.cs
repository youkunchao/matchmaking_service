using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetByUserIdAsync(Guid userId);
    Task<Order?> GetByOrderNoAsync(string orderNo);
    Task<List<Order>> GetByStatusAsync(string status);
}