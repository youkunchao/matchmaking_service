using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberPackageController : ControllerBase
{
    private readonly IMemberPackageService _packageService;

    public MemberPackageController(IMemberPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MemberPackage>>> GetAll()
    {
        var packages = await _packageService.GetAllPackagesAsync();
        return Ok(packages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberPackage>> GetById(Guid id)
    {
        var package = await _packageService.GetPackageByIdAsync(id);
        if (package == null)
            return NotFound();
        return Ok(package);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MemberPackage>> Create([FromBody] MemberPackage package)
    {
        try
        {
            var result = await _packageService.CreatePackageAsync(package);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MemberPackage>> Update(Guid id, [FromBody] MemberPackage package)
    {
        try
        {
            var result = await _packageService.UpdatePackageAsync(id, package);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _packageService.DeletePackageAsync(id);
        if (!result)
            return NotFound();
        return Ok();
    }
}