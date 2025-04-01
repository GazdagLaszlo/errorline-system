using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Administrator")]
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
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
    {
        var result = await userService.RegisterAsync(userCreateDto);
        return Ok(result);
    }

    [HttpPut]
    //[Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateDto userCreateDto)
    {
        var result = await userService.UpdateUserAsync(id, userCreateDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteUserAsync(id);
        return Ok();
    }
}