using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zhenghao.Matchmaking.Api.DTOs.Requests;
using Zhenghao.Matchmaking.Api.DTOs.Responses;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<UserResponse> RegisterAsync(UserRegisterRequest request)
    {
        if (await _userRepository.ExistsByOpenIdAsync(request.OpenId))
        {
            throw new Exception("该用户已注册");
        }

        var user = new User
        {
            OpenId = request.OpenId,
            UnionId = request.UnionId,
            Phone = request.Phone,
            Nickname = request.Nickname,
            Avatar = request.Avatar,
            Gender = request.Gender,
            Birthday = request.Birthday,
            Height = request.Height,
            Weight = request.Weight,
            Education = request.Education,
            Occupation = request.Occupation,
            Income = request.Income,
            MaritalStatus = request.MaritalStatus,
            City = request.City,
            District = request.District,
            Address = request.Address
        };

        var createdUser = await _userRepository.AddAsync(user);
        return UserResponse.FromEntity(createdUser);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByOpenIdAsync(request.OpenId);
        
        if (user == null)
        {
            throw new Exception("用户不存在");
        }

        var token = GenerateJwtToken(user);
        var expiresIn = _configuration.GetValue<int>("JwtSettings:ExpiresInMinutes");

        return new LoginResponse
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = expiresIn * 60,
            User = UserResponse.FromEntity(user)
        };
    }

    public async Task<UserResponse> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("用户不存在");
        }
        return UserResponse.FromEntity(user);
    }

    public async Task<UserResponse> UpdateUserAsync(Guid id, UserUpdateRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("用户不存在");
        }

        if (request.Nickname != null) user.Nickname = request.Nickname;
        if (request.Avatar != null) user.Avatar = request.Avatar;
        if (request.Gender.HasValue) user.Gender = request.Gender.Value;
        if (request.Birthday.HasValue) user.Birthday = request.Birthday.Value;
        if (request.Height.HasValue) user.Height = request.Height.Value;
        if (request.Weight.HasValue) user.Weight = request.Weight.Value;
        if (request.Education != null) user.Education = request.Education;
        if (request.Occupation != null) user.Occupation = request.Occupation;
        if (request.Income != null) user.Income = request.Income;
        if (request.MaritalStatus != null) user.MaritalStatus = request.MaritalStatus;
        if (request.City != null) user.City = request.City;
        if (request.District != null) user.District = request.District;
        if (request.Address != null) user.Address = request.Address;
        if (request.LivingStatus != null) user.LivingStatus = request.LivingStatus;
        if (request.Smoking != null) user.Smoking = request.Smoking;
        if (request.Drinking != null) user.Drinking = request.Drinking;
        if (request.ChildrenWish != null) user.ChildrenWish = request.ChildrenWish;
        if (request.MarriagePlan != null) user.MarriagePlan = request.MarriagePlan;
        if (request.SelfIntro != null) user.SelfIntro = request.SelfIntro;
        if (request.HobbyTags != null) user.HobbyTags = request.HobbyTags;
        if (request.CharacterTags != null) user.CharacterTags = request.CharacterTags;
        if (request.LoveViewTags != null) user.LoveViewTags = request.LoveViewTags;

        user.UpdatedAt = DateTime.Now;

        var updatedUser = await _userRepository.UpdateAsync(user);
        return UserResponse.FromEntity(updatedUser);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception("用户不存在");
        }
        await _userRepository.DeleteAsync(id);
    }

    public async Task<List<UserResponse>> SearchUsersAsync(Dictionary<string, object> conditions)
    {
        var users = await _userRepository.GetByConditionsAsync(conditions);
        return users.Select(UserResponse.FromEntity).ToList();
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Nickname ?? ""),
            new Claim("role", "user")
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.GetValue<int>("ExpiresInMinutes")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}