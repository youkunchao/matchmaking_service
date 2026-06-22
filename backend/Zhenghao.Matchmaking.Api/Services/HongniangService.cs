using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class HongniangService : IHongniangService
{
    private readonly IHongniangRepository _hongniangRepository;

    public HongniangService(IHongniangRepository hongniangRepository)
    {
        _hongniangRepository = hongniangRepository;
    }

    public async Task<HongniangResponse> GetHongniangByIdAsync(Guid id)
    {
        var hongniang = await _hongniangRepository.GetByIdAsync(id);
        if (hongniang == null)
        {
            throw new Exception("红娘不存在");
        }
        return HongniangResponse.FromEntity(hongniang);
    }

    public async Task<List<HongniangResponse>> GetActiveHongniangAsync()
    {
        var hongniangs = await _hongniangRepository.GetActiveHongniangAsync();
        return hongniangs.Select(HongniangResponse.FromEntity).ToList();
    }

    public async Task<List<UserResponse>> GetHongniangUsersAsync(Guid hongniangId)
    {
        var users = await _hongniangRepository.GetUsersByHongniangIdAsync(hongniangId);
        return users.Select(UserResponse.FromEntity).ToList();
    }
}