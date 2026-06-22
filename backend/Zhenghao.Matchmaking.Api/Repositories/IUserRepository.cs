using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByOpenIdAsync(string openId);
    Task<User?> GetByPhoneAsync(string phone);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetByConditionsAsync(Dictionary<string, object> conditions);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByOpenIdAsync(string openId);
    Task<bool> ExistsByPhoneAsync(string phone);
}