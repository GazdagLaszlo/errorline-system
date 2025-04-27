using ErrorlineSystem.Services;
using ErrorlineSystem.DataContext.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class MaintenanceManagerController(IMaintenanceManagerService maintenanceManagerService) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "MaintenanceManager")]
    [ProducesResponseType<IList<IssueDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOpenIssues()
    {
        var issues = await maintenanceManagerService.GetOpenIssuesAsync();
        return Ok(issues);
    }

    
    [HttpPost]
    //[Authorize(Roles = "MaintenanceManager")]
    [ProducesResponseType<IssueAssignDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> AssignIssue(int issueId, int assignedUserId)
    {
        var assign = await maintenanceManagerService.AssignIssueAsync(issueId, assignedUserId);
        return Ok(assign);
    }

    [HttpPut]
    //[Authorize(Roles = "MaintenanceManager")]
    [ProducesResponseType<IssueDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateIssueState(int issueId, IssueState state)
    {
        var result = await maintenanceManagerService.UpdateIssueStateAsync(issueId, state);
        return Ok(result);
    }
}