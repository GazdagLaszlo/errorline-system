using ErrorlineSystem.Services;
using ErrorlineSystem.DataContext.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class MaintenanceManagerController(IMaintenanceManagerService maintenanceManagerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOpenIssues()
    {
        var issues = await maintenanceManagerService.GetOpenIssuesAsync();
        return Ok(issues);
    }
}