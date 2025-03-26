using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Mvc;
 
namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EquipmentController(IEquipmentService equipmentService) : ControllerBase
{
        
    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
        var result = await equipmentService.GetAllAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquipmentCreateDto equipmentCreateDto)
    {
        var result = await equipmentService.CreateAsync(equipmentCreateDto);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await equipmentService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EquipmentCreateDto equipmentUpdateDto)
    {
        await equipmentService.UpdateAsync(id, equipmentUpdateDto);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await equipmentService.DeleteAsync(id);
        return Ok();
    }
}
