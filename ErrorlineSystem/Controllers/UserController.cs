using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Mvc;


namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult List()
    {
        var result = userService.List();
        return Ok(result);
    }
}