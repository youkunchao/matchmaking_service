using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class MemberPackageService : IMemberPackageService
{
    private readonly IMemberPackageRepository _packageRepository;

    public MemberPackageService(IMemberPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<List<MemberPackage>> GetAllPackagesAsync()
    {
        return await _packageRepository.GetActivePackagesAsync();
    }

    public async Task<MemberPackage?> GetPackageByIdAsync(Guid id)
    {
        return await _packageRepository.GetPackageByIdAsync(id);
    }

    public async Task<MemberPackage> CreatePackageAsync(MemberPackage package)
    {
        package.CreatedAt = DateTime.Now;
        package.UpdatedAt = DateTime.Now;
        return await _packageRepository.AddAsync(package);
    }

    public async Task<MemberPackage> UpdatePackageAsync(Guid id, MemberPackage package)
    {
        var existingPackage = await _packageRepository.GetPackageByIdAsync(id);
        if (existingPackage == null)
            throw new Exception("套餐不存在");

        existingPackage.Name = package.Name;
        existingPackage.DurationMonths = package.DurationMonths;
        existingPackage.Price = package.Price;
        existingPackage.Description = package.Description;
        existingPackage.Features = package.Features;
        existingPackage.SortOrder = package.SortOrder;
        existingPackage.Status = package.Status;
        existingPackage.UpdatedAt = DateTime.Now;

        return await _packageRepository.UpdateAsync(existingPackage);
    }

    public async Task<bool> DeletePackageAsync(Guid id)
    {
        var package = await _packageRepository.GetPackageByIdAsync(id);
        if (package == null)
            return false;

        await _packageRepository.DeleteAsync(id);
        return true;
    }
}