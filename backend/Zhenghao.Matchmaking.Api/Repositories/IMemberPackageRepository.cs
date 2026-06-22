using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IMemberPackageRepository
{
    Task<List<MemberPackage>> GetActivePackagesAsync();
    Task<MemberPackage?> GetByIdAsync(Guid id);
    Task<MemberPackage?> GetPackageByIdAsync(Guid id);
    Task<MemberPackage> AddAsync(MemberPackage entity);
    Task<MemberPackage> UpdateAsync(MemberPackage entity);
    Task DeleteAsync(Guid id);
}