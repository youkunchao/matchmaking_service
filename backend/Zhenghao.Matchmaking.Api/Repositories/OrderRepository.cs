using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(o => o.UserId == userId).ToListAsync();
    }

    public async Task<Order?> GetByOrderNoAsync(string orderNo)
    {
        return await _dbSet.FirstOrDefaultAsync(o => o.OrderNo == orderNo);
    }

    public async Task<List<Order>> GetByStatusAsync(string status)
    {
        return await _dbSet.Where(o => o.Status == status).ToListAsync();
    }
}