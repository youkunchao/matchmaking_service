using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IHongniangService
{
    Task<HongniangResponse> GetHongniangByIdAsync(Guid id);
    Task<List<HongniangResponse>> GetActiveHongniangAsync();
    Task<List<UserResponse>> GetHongniangUsersAsync(Guid hongniangId);
}