using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class IssueTypeController(IIssueTypeService IssueTypeService) : ControllerBase
{
        
    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
        var result = await IssueTypeService.GetAllAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IssueTypeCreateDto issueTypeCreateDto)
    {
        var result = await IssueTypeService.CreateAsync(issueTypeCreateDto);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await IssueTypeService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] IssueTypeCreateDto issueTypeCreateDto)
    {
        await IssueTypeService.UpdateAsync(id, issueTypeCreateDto);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await IssueTypeService.DeleteAsync(id);
        return Ok();
    }
}