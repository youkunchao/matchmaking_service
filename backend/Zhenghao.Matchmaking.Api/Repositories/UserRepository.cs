using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByOpenIdAsync(string openId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.OpenId == openId);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Phone == phone);
    }

    public async Task<List<User>> GetByConditionsAsync(Dictionary<string, object> conditions)
    {
        var query = _dbSet.AsQueryable();

        foreach (var condition in conditions)
        {
            switch (condition.Key)
            {
                case "Gender":
                    query = query.Where(u => u.Gender == (int?)condition.Value);
                    break;
                case "MinAge":
                    var minAge = (int)condition.Value;
                    var minBirthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-minAge));
                    query = query.Where(u => u.Birthday <= minBirthday);
                    break;
                case "MaxAge":
                    var maxAge = (int)condition.Value;
                    var maxBirthday = DateOnly.FromDateTime(DateTime.Now.AddYears(-maxAge));
                    query = query.Where(u => u.Birthday >= maxBirthday);
                    break;
                case "MinHeight":
                    query = query.Where(u => u.Height >= (int?)condition.Value);
                    break;
                case "MaxHeight":
                    query = query.Where(u => u.Height <= (int?)condition.Value);
                    break;
                case "Education":
                    query = query.Where(u => u.Education == condition.Value.ToString());
                    break;
                case "MaritalStatus":
                    query = query.Where(u => u.MaritalStatus == condition.Value.ToString());
                    break;
                case "City":
                    query = query.Where(u => u.City == condition.Value.ToString());
                    break;
                case "Smoking":
                    query = query.Where(u => u.Smoking == condition.Value.ToString());
                    break;
                case "Drinking":
                    query = query.Where(u => u.Drinking == condition.Value.ToString());
                    break;
                case "ChildrenWish":
                    query = query.Where(u => u.ChildrenWish == condition.Value.ToString());
                    break;
                case "Status":
                    query = query.Where(u => u.Status == (int)condition.Value);
                    break;
            }
        }

        return await query.ToListAsync();
    }

    public async Task<bool> ExistsByOpenIdAsync(string openId)
    {
        return await _dbSet.AnyAsync(u => u.OpenId == openId);
    }

    public async Task<bool> ExistsByPhoneAsync(string phone)
    {
        return await _dbSet.AnyAsync(u => u.Phone == phone);
    }
}