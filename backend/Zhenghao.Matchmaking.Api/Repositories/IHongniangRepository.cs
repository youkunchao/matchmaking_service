using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IHongniangRepository : IRepository<Hongniang>
{
    Task<Hongniang?> GetByUsernameAsync(string username);
    Task<List<Hongniang>> GetActiveHongniangAsync();
    Task<List<User>> GetUsersByHongniangIdAsync(Guid hongniangId);
}