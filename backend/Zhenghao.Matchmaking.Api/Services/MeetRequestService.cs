using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class MeetRequestService : IMeetRequestService
{
    private readonly IMeetRequestRepository _meetRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMatchRepository _matchRepository;

    public MeetRequestService(IMeetRequestRepository meetRequestRepository, IUserRepository userRepository, IMatchRepository matchRepository)
    {
        _meetRequestRepository = meetRequestRepository;
        _userRepository = userRepository;
        _matchRepository = matchRepository;
    }

    public async Task<MeetRequestResponse> CreateMeetRequestAsync(Guid userId, MeetRequestDto request)
    {
        if (userId == request.TargetUserId)
        {
            throw new Exception("不能向自己发起约见");
        }

        var isMutual = await _matchRepository.GetMatchBetweenUsersAsync(userId, request.TargetUserId);
        if (isMutual == null || !isMutual.IsMutual)
        {
            throw new Exception("需要双方互感兴趣才能发起约见");
        }

        var existingRequest = await _meetRequestRepository.GetBetweenUsersAsync(userId, request.TargetUserId);
        if (existingRequest != null && existingRequest.Status != 3)
        {
            throw new Exception("已有待处理的约见请求");
        }

        var meetRequest = new MeetRequest
        {
            UserId = userId,
            TargetUserId = request.TargetUserId,
            MeetTime = request.MeetTime,
            Location = request.Location,
            Remark = request.Remark,
            Status = 0
        };

        var createdRequest = await _meetRequestRepository.AddAsync(meetRequest);
        var targetUser = await _userRepository.GetByIdAsync(request.TargetUserId);
        return MeetRequestResponse.FromEntity(createdRequest, targetUser);
    }

    public async Task<MeetRequestResponse> ConfirmMeetRequestAsync(Guid requestId, Guid userId)
    {
        var request = await _meetRequestRepository.GetByIdAsync(requestId);
        if (request == null)
        {
            throw new Exception("约见请求不存在");
        }

        if (request.TargetUserId != userId)
        {
            throw new Exception("只能确认自己收到的约见请求");
        }

        if (request.Status != 0)
        {
            throw new Exception("请求状态不允许确认");
        }

        request.Status = 1;
        request.UpdatedAt = DateTime.Now;

        var updatedRequest = await _meetRequestRepository.UpdateAsync(request);
        var targetUser = await _userRepository.GetByIdAsync(request.UserId);
        return MeetRequestResponse.FromEntity(updatedRequest, targetUser);
    }

    public async Task<MeetRequestResponse> CancelMeetRequestAsync(Guid requestId, Guid userId)
    {
        var request = await _meetRequestRepository.GetByIdAsync(requestId);
        if (request == null)
        {
            throw new Exception("约见请求不存在");
        }

        if (request.UserId != userId && request.TargetUserId != userId)
        {
            throw new Exception("只能取消自己发起或收到的约见请求");
        }

        if (request.Status == 2)
        {
            throw new Exception("已完成的请求不能取消");
        }

        request.Status = 3;
        request.UpdatedAt = DateTime.Now;

        var updatedRequest = await _meetRequestRepository.UpdateAsync(request);
        var targetUser = request.UserId == userId 
            ? await _userRepository.GetByIdAsync(request.TargetUserId)
            : await _userRepository.GetByIdAsync(request.UserId);
        return MeetRequestResponse.FromEntity(updatedRequest, targetUser);
    }

    public async Task<MeetRequestResponse> CompleteMeetRequestAsync(Guid requestId, Guid userId)
    {
        var request = await _meetRequestRepository.GetByIdAsync(requestId);
        if (request == null)
        {
            throw new Exception("约见请求不存在");
        }

        if (request.UserId != userId && request.TargetUserId != userId)
        {
            throw new Exception("只能完成自己参与的约见");
        }

        if (request.Status != 1)
        {
            throw new Exception("请求状态不允许完成");
        }

        request.Status = 2;
        request.UpdatedAt = DateTime.Now;

        var updatedRequest = await _meetRequestRepository.UpdateAsync(request);
        var targetUser = request.UserId == userId 
            ? await _userRepository.GetByIdAsync(request.TargetUserId)
            : await _userRepository.GetByIdAsync(request.UserId);
        return MeetRequestResponse.FromEntity(updatedRequest, targetUser);
    }

    public async Task<MeetRequestResponse> SubmitFeedbackAsync(FeedbackRequest request, Guid userId)
    {
        var meetRequest = await _meetRequestRepository.GetByIdAsync(request.MeetRequestId);
        if (meetRequest == null)
        {
            throw new Exception("约见请求不存在");
        }

        if (meetRequest.UserId != userId && meetRequest.TargetUserId != userId)
        {
            throw new Exception("只能对自己参与的约见提交反馈");
        }

        if (meetRequest.Status != 2)
        {
            throw new Exception("只能对已完成的约见提交反馈");
        }

        meetRequest.Feedback = request.Feedback;
        meetRequest.FeedbackScore = request.FeedbackScore;
        meetRequest.UpdatedAt = DateTime.Now;

        var updatedRequest = await _meetRequestRepository.UpdateAsync(meetRequest);
        var targetUser = meetRequest.UserId == userId 
            ? await _userRepository.GetByIdAsync(meetRequest.TargetUserId)
            : await _userRepository.GetByIdAsync(meetRequest.UserId);
        return MeetRequestResponse.FromEntity(updatedRequest, targetUser);
    }

    public async Task<List<MeetRequestResponse>> GetUserMeetRequestsAsync(Guid userId)
    {
        var requests = await _meetRequestRepository.GetByUserIdAsync(userId);
        var responses = new List<MeetRequestResponse>();

        foreach (var request in requests)
        {
            var targetUser = await _userRepository.GetByIdAsync(request.TargetUserId);
            responses.Add(MeetRequestResponse.FromEntity(request, targetUser));
        }

        return responses;
    }

    public async Task<List<MeetRequestResponse>> GetReceivedMeetRequestsAsync(Guid userId)
    {
        var requests = await _meetRequestRepository.GetByTargetUserIdAsync(userId);
        var responses = new List<MeetRequestResponse>();

        foreach (var request in requests)
        {
            var targetUser = await _userRepository.GetByIdAsync(request.UserId);
            responses.Add(MeetRequestResponse.FromEntity(request, targetUser));
        }

        return responses;
    }
}