using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IMemberPackageService
{
    Task<List<MemberPackage>> GetAllPackagesAsync();
    Task<MemberPackage?> GetPackageByIdAsync(Guid id);
    Task<MemberPackage> CreatePackageAsync(MemberPackage package);
    Task<MemberPackage> UpdatePackageAsync(Guid id, MemberPackage package);
    Task<bool> DeletePackageAsync(Guid id);
}