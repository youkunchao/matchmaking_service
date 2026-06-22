using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IMeetRequestService
{
    Task<MeetRequestResponse> CreateMeetRequestAsync(Guid userId, MeetRequestDto request);
    Task<MeetRequestResponse> ConfirmMeetRequestAsync(Guid requestId, Guid userId);
    Task<MeetRequestResponse> CancelMeetRequestAsync(Guid requestId, Guid userId);
    Task<MeetRequestResponse> CompleteMeetRequestAsync(Guid requestId, Guid userId);
    Task<MeetRequestResponse> SubmitFeedbackAsync(FeedbackRequest request, Guid userId);
    Task<List<MeetRequestResponse>> GetUserMeetRequestsAsync(Guid userId);
    Task<List<MeetRequestResponse>> GetReceivedMeetRequestsAsync(Guid userId);
}