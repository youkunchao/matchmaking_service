using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IMemberPackageRepository : IRepository<MemberPackage>
{
    Task<List<MemberPackage>> GetActivePackagesAsync();
    Task<MemberPackage?> GetPackageByIdAsync(Guid id);
}