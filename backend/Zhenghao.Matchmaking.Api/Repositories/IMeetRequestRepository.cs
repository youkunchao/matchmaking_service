using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IMeetRequestRepository : IRepository<MeetRequest>
{
    Task<List<MeetRequest>> GetByUserIdAsync(Guid userId);
    Task<List<MeetRequest>> GetByTargetUserIdAsync(Guid targetUserId);
    Task<List<MeetRequest>> GetByStatusAsync(int status);
    Task<MeetRequest?> GetBetweenUsersAsync(Guid userId, Guid targetUserId);
}