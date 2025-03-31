using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var order = await userService.GetAllUsers();
        return Ok(order);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
    {
        var token = await userService.LoginAsync(userDto);
        return Ok(new { Token = token });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] UserCreateDto userCreateDto)
    {
        var result = await userService.RegisterAsync(userCreateDto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserCreateDto userCreateDto)
    {
        var result = await userService.UpdateUserAsync(id, userCreateDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        await userService.DeleteUserAsync(id);
        return Ok();
    }
}