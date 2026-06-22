using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IUserService
{
    Task<UserResponse> RegisterAsync(UserRegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<UserResponse> GetUserByIdAsync(Guid id);
    Task<UserResponse> UpdateUserAsync(Guid id, UserUpdateRequest request);
    Task DeleteUserAsync(Guid id);
    Task<List<UserResponse>> SearchUsersAsync(Dictionary<string, object> conditions);
}